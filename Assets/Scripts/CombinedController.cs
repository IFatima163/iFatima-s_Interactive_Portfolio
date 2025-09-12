using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinedController : MonoBehaviour, Interactable
{
    [SerializeField] ImageDialog imageDialog;
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        StartCoroutine(CombinedManager.Instance.ShowAllImages(imageDialog, dialog));
    }
}
