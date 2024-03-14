using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public static ContentManager instance;
    public Root allData;
    public TextAsset CurrentJson;

    private string jsonPathNormal;

    public Valute[] Valutes;
    public SettingsInfo settingsInfo;
    public Valute ChoosedValute;

    public Countrys allCountrys;
    private void Awake()
    {
        jsonPathNormal = Application.persistentDataPath + "/obj.json";
        if (instance != null)
            Destroy(this);
        instance = this;
        Debug.Log(Application.persistentDataPath);
        StartCoroutine(GetAllData());
    }
    public TextAsset CountryJson;
    IEnumerator GetAllData()
    {
        string jsonContent;
        if (File.Exists(jsonPathNormal))
        {
            jsonContent = File.ReadAllText(jsonPathNormal);
        }
        else
        {
            jsonContent = CurrentJson.ToString();
        }
        allData = JsonUtility.FromJson<Root>(jsonContent);

        yield return allData;
        if (allData.valute.valute != "" && allData.valute.valute != null)
            ChoosedValute = allData.valute;
        else ChoosedValute = Valutes[0];
        settingsInfo = allData.settingsInfo;
        string countryJsonContent = CountryJson.ToString();
        allCountrys = JsonUtility.FromJson<Countrys>(countryJsonContent);
        FillDataInDictionary();
    }
    public static Dictionary<string, Dictionary<string, List<Obj>>> allDataDic 
    { get; private set; } = new Dictionary<string, Dictionary<string, List<Obj>>>(); //Dictionary<type, Dictionary<obj.name, Obj>>

    private void FillDataInDictionary()
    {
        foreach (var category in allData.category)
        {
            Dictionary<string, List<Obj>> objDic = new Dictionary<string, List<Obj>>();
            foreach (var localCategory in category.localCategory)
            {
                objDic.Add(localCategory.localCategoryname, CreateLocalCategoryDic(localCategory));
            }
            allDataDic.Add(category.categoryName, objDic);
        }
    }
    private List<Obj> CreateLocalCategoryDic(LocalCategory localCategory)
    {
        List<Obj> objDic = new List<Obj>();
        foreach (Obj obj in localCategory.obj)
        {
            if (obj.Saved == null) 
            {
                obj.Saved = new List<Saved>
                {
                    new Saved
                    {
                        ColorName = obj.imageLinks[0],
                        sizes = new List<Size>()
                    }
                };
            }
            if (obj.Saved.Count == 0)
            {
                obj.Saved.Add(new Saved 
                { 
                    ColorName = obj.imageLinks[0],
                    sizes = new List<Size>()
                });
            }
                objDic.Add(obj);
        }
        return objDic;
    }
    public void StartSaveCoroutine()
    {
        StartCoroutine(SaveToJson());
    }
    private IEnumerator SaveToJson()
    {
        Root newData = new();
        newData.category = new List<Category>();
        var category = newData.category;
        foreach(var dicCategory in allDataDic)
        {
            category.Add(
                new Category
                {
                    categoryName = dicCategory.Key,
                    localCategory = new List<LocalCategory>()
                });
            var localCategory = category[category.Count - 1].localCategory;
            foreach (var dicLocalCategory in dicCategory.Value)
            {
                localCategory.Add(new LocalCategory
                {
                    localCategoryname = dicLocalCategory.Key,
                    obj = dicLocalCategory.Value
                });
            }
        }
        newData.settingsInfo = settingsInfo;
        newData.valute = ChoosedValute;
        string json = JsonUtility.ToJson(newData);
        yield return json;
        //Debug.Log(Application.dataPath + "/" + "obj");
        try
        {
            File.WriteAllText(instance.jsonPathNormal, json);

        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void OnApplicationQuit()
    {
        StartSaveCoroutine();
    }
    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartSaveCoroutine();
        }
    }
}
