using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValuteButton : MonoBehaviour
{
    public Valute valute;
    public GameObject CheckMark;
    public TextMeshProUGUI nameTMP;
    public void OnClick()
    {
        ChangeValute.Instance.ChangeValuteInSave(valute);
    }
}
