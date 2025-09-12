using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public Image displayImage; // UI Image component to display the sprites
    public event Action OnShowImage;
    public event Action OnHideImage;

    public static ImageManager Instance { get; private set; }

    private ImageDialog imageDialog;
    private int currentIndex = 0;
    private bool allImagesShown = false;

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

    public IEnumerator ShowImage(ImageDialog dialog)
    {
        imageDialog = dialog;
        currentIndex = 0;
        allImagesShown = false;

        if (imageDialog.Images.Count > 0)
        {
            OnShowImage?.Invoke();
            displayImage.gameObject.SetActive(true);
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
            displayImage.enabled = true;
            yield return null; // Ensure this runs as a coroutine
        }
        else
        {
            displayImage.gameObject.SetActive(false);
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && displayImage.gameObject.activeSelf)
        {
            NextImage();
        }
    }

    private void NextImage()
    {
        if (allImagesShown || imageDialog.Images.Count == 0)
        {
            OnHideImage?.Invoke();
            audioManager.PlaySFX(audioManager.image_change);
            displayImage.gameObject.SetActive(false);
            return;
        }

        currentIndex++;
        if (currentIndex < imageDialog.Images.Count)
        {
            displayImage.sprite = imageDialog.Images[currentIndex];
            audioManager.PlaySFX(audioManager.image_change);
        }
        else
        {
            displayImage.enabled = false;
            displayImage.gameObject.SetActive(false);
            allImagesShown = true;            
            audioManager.PlaySFX(audioManager.image_change);
            OnHideImage?.Invoke();
        }
    }
}