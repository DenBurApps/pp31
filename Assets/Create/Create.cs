using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Create : MonoBehaviour
{
    public static Root allData;
    public TextAsset CurrentJson;
    private void Awake()
    {
        StartCoroutine(GetAllData());
    }
    IEnumerator GetAllData()
    {
        string jsonContent = CurrentJson.ToString();
        allData = JsonUtility.FromJson<Root>(jsonContent);
        yield return allData;
        int j = 0;
        int[] prices = { 20, 25, 30, 35 };
        int[] discounts = { 0, 0,0,10, 20, 30 };

        foreach (var item in allData.category)
        {
            foreach (var obj in item.localCategory)
            {
                foreach (var objl in obj.obj)
                {
                    int price = prices[Random.Range(0, prices.Length)];
                    int discount = discounts[Random.Range(0, discounts.Length)];

                    objl.price = price;
                    objl.discountCount = discount;

                    objl.isNew = System.Convert.ToBoolean(Random.Range(0, 2));
                    objl.mostPopular = System.Convert.ToBoolean(Random.Range(0, 2));
                    j++;
                    
                }
            }
        }
        var json = JsonUtility.ToJson(allData);
        File.WriteAllText(Application.dataPath + "/Create/" + "obj.json", json);
    }
}
