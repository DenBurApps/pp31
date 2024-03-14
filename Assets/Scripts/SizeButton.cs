using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeButton : MonoBehaviour
{

    public string size;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI[] sizeText;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        foreach (var text in sizeText)
            text.text = size;
    }
    public void DisableButton()
    {
        button.gameObject.SetActive(false);
    }
    public void EnableButton()
    {
        button.gameObject.SetActive(true);
    }
    public void OnClick()
    {
        Preview.Instance.ChooseSize(size);
    }
}
