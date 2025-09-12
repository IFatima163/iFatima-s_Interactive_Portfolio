using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternateController : MonoBehaviour, Interactable
{
    [SerializeField] ImageDialog imageDialog;
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        StartCoroutine(AlternateManager.Instance.ShowImage(imageDialog, dialog));
    }
}
