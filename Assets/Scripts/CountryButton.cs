using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountryButton : MonoBehaviour
{
    public string CountryName;
    public GameObject CheckMark;
    public TextMeshProUGUI nameTMP;
    public void OnClick()
    {
        ChooseAddress.Instance.ChangeCountryInSave(CountryName);
    }
}
