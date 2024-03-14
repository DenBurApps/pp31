using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomDropdown : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    [SerializeField] Vector2 openedize;
    [SerializeField] GameObject arrow;
    [SerializeField] private Color openedColor;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        opened = true;
        OnClick();
    }
    private bool opened;
    private void OnClick()
    {
        if (opened)
        {
            rt.sizeDelta = GetComponent<RectTransform>().sizeDelta;
            opened = false;
            arrow.transform.localRotation = new Quaternion(0,0,180,0);
            arrow.GetComponent<Image>().color = Color.black;
        }
        else
        {
            rt.sizeDelta = openedize;
            opened = true;
            arrow.transform.localRotation = new Quaternion(0, 0, 0, 0);
            arrow.GetComponent<Image>().color = openedColor;
        }
    }
}
