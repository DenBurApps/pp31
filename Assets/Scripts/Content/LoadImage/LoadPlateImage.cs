using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlateImage : MonoBehaviour
{
    private string _imageLink;

    private Action<Texture2D> OnSpawned;

    [SerializeField]
    private RawImage PlateImg;
    [SerializeField]
    private GameObject _loadingObj;
    [SerializeField]
    private Vector2 startingImageSize = new Vector2(0,0);

    private void OnEnable()
    {
      
        OnSpawned += GetTexture;
          
    }
    private void OnDisable()
    {
      
        OnSpawned -= GetTexture;
      
    }
    private void OnDestroy()
    {
        //Destroy(PlateImg.texture);
    }

    public void LoadPlateImageInRawImage(string imageLink, string imageName)
    {
        _loadingObj.SetActive(true);
        _imageLink = imageLink;
        CheckImageFileExists(imageName);

    }

    public void GetTexture(Texture2D texture)
    {
        var SD = PlateImg.rectTransform.sizeDelta;

        if (startingImageSize == new Vector2(0,0))
        {
            startingImageSize = new Vector2(SD.x, SD.y);
        }
        else
            PlateImg.rectTransform.sizeDelta = startingImageSize;

        PlateImg.texture = texture;

        var size = PlateImg.rectTransform;
        float loadPlateImageWidth = PlateImg.rectTransform.rect.width;
        float height = PlateImg.rectTransform.rect.height;

        float differenceInImage = height / texture.height;
        float normalWidth = texture.width * differenceInImage;
        float normalHeight = texture.height * differenceInImage;

        if (normalWidth < loadPlateImageWidth)
        {
            differenceInImage = loadPlateImageWidth / normalWidth;
            normalWidth *= differenceInImage;
            normalHeight *= differenceInImage;
        }

        size.sizeDelta = new Vector2(normalWidth, normalHeight);

        _loadingObj.SetActive(false);

        OnSpawned -= GetTexture;
    }
      
    public void CheckImageFileExists(string textureName)
    {
        Texture2D texture = null;
        foreach(Texture2D t in ImagesHolder.Instance.image)
        {
            if(t.name == textureName)
                texture = t;
        }
        GetTexture(texture);
        //StartCoroutine(ConvertToTexture.Convert(" ", OnSpawned, texture));
        /*        string fullImageLink = Application.dataPath + "/Images/" + _imageLink + ".png";

                if (File.Exists(fullImageLink))
                {
                    StartCoroutine(ConvertToTexture.Convert(fullImageLink, OnSpawned));
                }
                else
                {
                    Debug.Log("not founded - " + fullImageLink);
                }*/
    }
}
