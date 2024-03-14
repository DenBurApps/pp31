using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Send : MonoBehaviour
{
    public TMP_InputField MessageSub;
    public TMP_InputField Mail;
    public TMP_InputField MessageText;

    public Button SendButton;

    private void Awake()
    {
        MessageSub.onValueChanged.AddListener(CheckSend);
        Mail.onValueChanged.AddListener(CheckSend);
        MessageText.onValueChanged.AddListener(CheckSend);
    }
    public void CheckSend(string str)
    {
        if(MessageSub.text != "" && Mail.text != "" && MessageText.text != "") 
        {
            SendButton.interactable = true;
        }
        else
            SendButton.interactable = false;
    }
}
