using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class ImageDialog
{
    [SerializeField] List<Sprite> images;

    public List<Sprite> Images
    {
        get { return images; }
    }
}