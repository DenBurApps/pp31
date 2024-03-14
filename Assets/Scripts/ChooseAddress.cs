using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAddress : MonoBehaviour
{
    public static ChooseAddress Instance;

    private List<CountryButton> countryButtons = new List<CountryButton>();

    public SettingsInfo settingsInfo;
    public TMP_InputField inputFieldAddress;
    public TMP_InputField inputFieldTown;
    public TMP_InputField inputFieldPostCode;

    public GameObject CloseButtons;
    public TextMeshProUGUI CountryTMP;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        settingsInfo = ContentManager.instance.settingsInfo;
        CloseButtons.SetActive(true);
        SpawnAllCoutnryButtons();
        foreach (CountryButton button in countryButtons)
        {
            if (button.CountryName == ContentManager.instance.settingsInfo.choosedCountry)
                button.OnClick();
            button.GetComponent<Button>().onClick.AddListener(() => { CloseButtons.SetActive(false); });
        }

        CloseButtons.SetActive(false);
        inputFieldAddress.onValueChanged.AddListener(ChangeAddress);
        inputFieldTown.onValueChanged.AddListener(ChangeTown);
        inputFieldPostCode.onValueChanged.AddListener(ChangePostcode);

        inputFieldAddress.text = settingsInfo.address;
        inputFieldTown.text = settingsInfo.town;
        inputFieldPostCode.text = settingsInfo.postcode;
        CountryTMP.text = settingsInfo.choosedCountry;
    }
    public void ChangeAddress(string str)
    {
        settingsInfo.address = str;
    }
    public void ChangeTown(string str)
    {
        settingsInfo.town = str;
    }
    public void ChangePostcode(string str)
    {
        settingsInfo.postcode = str;
    }
    public void SaveAll()
    {
        ContentManager.instance.settingsInfo = settingsInfo;
        ContentManager.instance.StartSaveCoroutine();
    }
    public void ChangeCountryInSave(string name)
    {
        settingsInfo.choosedCountry = name;
        foreach (CountryButton button in countryButtons)
        {
            if (button.CountryName == name)
            {
                button.CheckMark.SetActive(true);
                Debug.Log("1");
            }
            else
                button.CheckMark.SetActive(false);
        }
        CountryTMP.text = settingsInfo.choosedCountry;
    }

    [SerializeField] private CountryButton countryPrefab;
    [SerializeField] private Transform prefabSpawnPoint;

    private void SpawnAllCoutnryButtons()
    {
        foreach (var country in ContentManager.instance.allCountrys.Country)
        {
            var obj = Instantiate(countryPrefab, prefabSpawnPoint);
            obj.CountryName = country.name;
            obj.nameTMP.text = country.name;
            countryButtons.Add(obj);
        }
    }
}
