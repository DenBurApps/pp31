using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculateCost : MonoBehaviour
{
    public static CalculateCost Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }
    private float allSum;
    private int allObjCount;
    [SerializeField]
    private TextMeshProUGUI priceTMP;
    [SerializeField]
    private TextMeshProUGUI objCountTMP;


    [SerializeField]
    private SpawnPlatesManager spawnPlatesManager;
    public void Calculate()
    {
        allSum = 0;
        allObjCount = 0;
        foreach (var obj in spawnPlatesManager._spawnedPlates)
        {
            var plateData = obj.GetComponent<PlateData>();
            if (plateData.Properties.discountCount > 0)
            {
                allSum += (plateData.Properties.price - ((float)plateData.Properties.discountCount / 100 * plateData.Properties.price)) * ContentManager.instance.ChoosedValute.multiplier * plateData.ReturnInBagCount();
            }
            else
                allSum += plateData.Properties.price * ContentManager.instance.ChoosedValute.multiplier * plateData.ReturnInBagCount();
            allObjCount += plateData.ReturnInBagCount();
        }

        priceTMP.text = "Total " + ContentManager.instance.ChoosedValute.valute + allSum.ToString();
        objCountTMP.text = allObjCount.ToString();
    }
}
