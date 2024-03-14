using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomPanel : MonoBehaviour
{
    public static BottomPanel instance;
    [Serializable]
    private struct buttonsAndWindows
    {
        public Button button;
        public GameObject notButton;
        public GameObject window;
    }
    [SerializeField]
    private buttonsAndWindows[] BAW;
    private void Awake()
    {
        instance = this;
        foreach (var obj in BAW)
        {
            obj.button.onClick.AddListener(() =>
            OnClick(obj.notButton, obj.button.gameObject, obj.window));
        }
    }
    private void OnClick(GameObject activate, GameObject disable, GameObject window)
    {
        foreach(var obj in BAW)
        {
            obj.button.gameObject.SetActive(true);
            obj.notButton.gameObject.SetActive(false);
            obj.window.SetActive(false);
        }
        window.SetActive(true);
        activate.SetActive(true);
        disable.SetActive(false);
    }

    public void ChangeWindow(int windowNumber)
    {
        var obj = BAW[windowNumber];
        OnClick(obj.notButton, obj.button.gameObject, obj.window);
    }
}
