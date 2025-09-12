using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour, Interactable
{
    [SerializeField] ImageDialog imageDialog;
 
    public void Interact()
    {
        StartCoroutine(ImageManager.Instance.ShowImage(imageDialog));
    }
}