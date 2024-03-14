using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagesHolder : MonoBehaviour
{
    public static ImagesHolder Instance;
    public Texture2D[] image;
    private void Awake()
    {
        Instance = this;
        //Debug.Log(image.Length);
    }
}
