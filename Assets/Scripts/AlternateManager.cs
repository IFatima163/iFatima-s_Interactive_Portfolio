using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternateManager : MonoBehaviour
{
    public Image displayImage; // UI Image component to display the sprites
    public GameObject dialogBox; // Dialog box UI
    public Text dialogText; // Dialog text UI
    public int lettersPerSecond;
    public event Action OnAlternateShow;
    public event Action OnAlternateHide;

    public static AlternateManager Instance { get; private set; }

    private ImageDialog imageDialog;
    private Dialog dialog;
    private int currentIndex;
    private int currentLine;
    private int imagesShown;
    private int dialogsShown;
    private bool allImagesShown;
    private bool allDialogsShown;
    private bool isTyping;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ShowImage(ImageDialog imageDialog, Dialog dialog)
    {
        this.imageDialog = imageDialog;
        this.dialog = dialog;
        currentIndex = 0;
        currentLine = 0;
        imagesShown = 0;
        dialogsShown = 0;
        allImagesShown = false;
        allDialogsShown = false;

        if (imageDialog.Images.Count > 0)
        {
            OnAlternateShow?.Invoke();
            displayImage.gameObject.SetActive(true);
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
            displayImage.enabled = true;
            imagesShown++;
            yield return null;
        }
        else
        {   
            displayImage.gameObject.SetActive(false);
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping)
        {
            if (!allDialogsShown || !allImagesShown) 
            {
                if (imagesShown == dialogsShown)
                {
                    ContinueImages();
                }
                else
                {
                    if (dialogsShown == 0)
                    {
                        StartCoroutine(ShowDialog());
                    }
                    else
                    {
                        ContinueDialogs();
                    }
                }
            }
        }
    }
    
    private void ContinueImages()
    {
        currentIndex++;
        if (currentIndex < imageDialog.Images.Count)
        {
            dialogBox.SetActive(false);
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
            imagesShown++;
        }
        else
        {
            allImagesShown = true;
            ContinueDialogs();
        }
    }

    private void ContinueDialogs()
    {
        currentLine++;
        if (currentLine < dialog.Lines.Count)
        {
            StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
        }
        else
        {
            audioManager.PlaySFX(audioManager.image_change);
            allDialogsShown = true;
            dialogBox.SetActive(false);
            displayImage.enabled = false;
            displayImage.gameObject.SetActive(false);
            OnAlternateHide?.Invoke();
        }
    }

    private IEnumerator ShowDialog()
    {
        yield return new WaitForEndOfFrame();
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
    }

    private IEnumerator TypeDialog(string line)
    {
        dialogBox.SetActive(true);
        isTyping = true;
        dialogText.text = "";
        audioManager.PlaySFX(audioManager.typing);
        dialogsShown++;
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        audioManager.StopSFX();
        isTyping = false;
    }
}
