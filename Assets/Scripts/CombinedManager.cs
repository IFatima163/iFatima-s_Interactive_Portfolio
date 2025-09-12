using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinedManager : MonoBehaviour
{
    public Image displayImage; // UI Image component to display the sprites
    public GameObject dialogBox; // Dialog box UI
    public Text dialogText; // Dialog text UI
    public int lettersPerSecond;
    public event Action OnCombinedShow;
    public event Action OnCombinedHide;

    public static CombinedManager Instance { get; private set; }

    private ImageDialog imageDialog;
    private Dialog dialog;
    private int currentIndex;
    private int currentLine;
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

    public IEnumerator ShowAllImages(ImageDialog imageDialog, Dialog dialog)
    {
        this.imageDialog = imageDialog;
        this.dialog = dialog;
        currentIndex = 0;
        currentLine = 0;
        allImagesShown = false;
        allDialogsShown = false;

        if (imageDialog.Images.Count > 0)
        {
            OnCombinedShow?.Invoke();
            displayImage.gameObject.SetActive(true);
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
            displayImage.enabled = true;
            yield return null;
        }
        else
        {
            displayImage.gameObject.SetActive(false);
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!allImagesShown)
            {
                ContinueImages();
            }
            else if (!isTyping && allImagesShown && !allDialogsShown)
            {
                ContinueDialogs();
            }
        }
    }

    private void ContinueImages()
    {
        currentIndex++;
        if (currentIndex < imageDialog.Images.Count)
        {
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
        }
        else
        {
            allImagesShown = true;
            StartCoroutine(ShowDialog());
        }
    }

    private void ContinueDialogs()
    {
        ++currentLine;
        if (currentLine < dialog.Lines.Count)
        {
            StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
        }
        else
        {
            dialogBox.SetActive(false);
            allDialogsShown = true;
            currentLine = 0;
            displayImage.enabled = false;
            displayImage.gameObject.SetActive(false);            
            audioManager.PlaySFX(audioManager.image_change);
            OnCombinedHide?.Invoke();   
        }
    }

    private IEnumerator ShowDialog()
    {
        yield return new WaitForEndOfFrame();
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    private IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        audioManager.PlaySFX(audioManager.typing);
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        audioManager.StopSFX();
        isTyping = false;
    }
}
