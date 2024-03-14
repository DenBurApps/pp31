using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{

    [HideInInspector] public string color;
    [SerializeField] private Button button;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private LoadPlateImage loadPlateImage;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    public void SetData(string color)
    {
        this.color = color;
        if (color != null && color != "")
            loadPlateImage.LoadPlateImageInRawImage(color, color);
    }
    public void DisableButton()
    {
        checkMark.SetActive(true);
    }
    public void EnableButton()
    {
        checkMark.SetActive(false);
    }
    public void OnClick()
    {
        Preview.Instance.ChooseColor(color);
    }
}
