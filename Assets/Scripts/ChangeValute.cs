using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeValute : MonoBehaviour
{
    public static ChangeValute Instance;

    private List<ValuteButton> valuteButtons = new List<ValuteButton>();
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        SpawnAllValuteButtons();
        foreach(ValuteButton button in valuteButtons)
            if(button.valute.multiplier == ContentManager.instance.ChoosedValute.multiplier)
            {
                button.OnClick();
                break;
            } 
    }
    public void ChangeValuteInSave(Valute valute)
    {
        ContentManager.instance.ChoosedValute = valute;
        ContentManager.instance.StartSaveCoroutine();
        foreach (ValuteButton button in valuteButtons)
        {
            if (button.valute.multiplier == valute.multiplier)
                button.CheckMark.SetActive(true);
            else
                button.CheckMark.SetActive(false);
        }
    }

    [SerializeField] private ValuteButton valutePrefab;
    [SerializeField] private Transform prefabSpawnPoint;

    private void SpawnAllValuteButtons()
    {
        foreach(var valute in ContentManager.instance.Valutes)
        {
            var obj = Instantiate(valutePrefab,prefabSpawnPoint);
            obj.valute = valute;
            obj.nameTMP.text = valute.valuteFullName;
            valuteButtons.Add(obj);
        }
    }
}
