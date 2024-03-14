using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlatesManager : MonoBehaviour
{
    [SerializeField]
    private bool canSpawnOnStart;
    [SerializeField]
    private PlateData _platePrefab;

    [SerializeField]
    public List<PlateData> _spawnedPlates = new List<PlateData>();
    [SerializeField]
    private Transform spawnTransform;
    [SerializeField]
    private TextMeshProUGUI screenName;

    [SerializeField]
    private string category;
    [SerializeField]
    private string localCategory;
    [SerializeField]
    private categoryEnum spawnMethod = categoryEnum.standart;

    [SerializeField] private GameObject enableIfEmpty;
    [SerializeField] private Button BuyButton;
    public void OnEnable()
    {
        if (canSpawnOnStart)
        {
            ClearSpawnedPlates();
            StartSpawn();
            if(enableIfEmpty != null)
            {
                if (_spawnedPlates.Count == 0)
                {
                    enableIfEmpty.SetActive(true);
                    if(BuyButton != null)
                        BuyButton.interactable = false;
                }
                else
                {
                    enableIfEmpty.SetActive(false);
                    if (BuyButton != null)
                        BuyButton.interactable = true;
                }
            }
        }
        CalculateCost.Instance.Calculate();
    }

    private void DeleteManyPlates()
    {
        if(maximumSpawnPlates > 0)
        {
            if(_spawnedPlates.Count > maximumSpawnPlates)
            {
                int i = Random.Range(0, _spawnedPlates.Count);
                Destroy(_spawnedPlates[i].gameObject);
                _spawnedPlates.RemoveAt(i);
                DeleteManyPlates();
            }
        }
    }
    public enum categoryEnum
    {
        standart,
        allCategory,
        discount,
        isNew,
        mostPopular,
        wishlist,
        inBag,
    }
    public void ChangeCategory(string category,string localCategory)
    {
        this.category = category;
        this.localCategory = localCategory;
        ClearSpawnedPlates();
        StartSpawn();
    }
    private void ClearSpawnedPlates()
    {
        foreach (Transform item in spawnTransform)
        {
            Destroy(item.gameObject);
        }
        _spawnedPlates.Clear();
    }
    private void StartSpawn()
    {
        switch (spawnMethod)
        {
            case categoryEnum.standart:
                SpawnPlates();
                break;
            case categoryEnum.allCategory:
                SpawnAllCategoryPlates();
                break;
            case categoryEnum.discount:
                SpawnPlatesInDiscount();
                break;
            case categoryEnum.isNew:
                SpawnPlatesInIsNew();
                break;
            case categoryEnum.mostPopular:
                SpawnPlatesInMostPopular();
                break;
            case categoryEnum.wishlist:
                SpawnPlatesInWishlist();
                break;
            case categoryEnum.inBag:
                SpawnPlatesInCart();
                break;
            default:
                Debug.LogError("что-то пошло не так");
                break;
        }
        DeleteManyPlates();
    }
    [SerializeField]
    private int maximumSpawnPlates;
    private void SpawnPlates()
    {
        var dic = ContentManager.allDataDic[category][localCategory];
        for (int i = 0; i < dic.Count; i++)
        {
            SpawnOnePlate().SetDataInPlate(dic[i], dic[i].Saved[0],0);
        }
    }
    private void SpawnAllCategoryPlates()
    {
        var dic = ContentManager.allDataDic[category];
        foreach (var item in dic)
        for (int i = 0; i < dic.Count; i++)
        {
            SpawnOnePlate().SetDataInPlate(item.Value[i], item.Value[i].Saved[0], 0);
        }
    }
    private void SpawnPlatesInWishlist()
    {
        foreach (var category in ContentManager.allDataDic)
            foreach (var localCategory in category.Value)
                foreach (var obj in localCategory.Value)
                { 
                    int i = 0;
                    foreach (var item in obj.Saved)
                    {
                        if (item.favorite)
                            SpawnOnePlate().SetDataInPlateWishlist(obj, item, i, this);
                        i++;
                    }
                }
    }
    private void SpawnPlatesInDiscount()
    {
        foreach (var category in ContentManager.allDataDic)
            foreach (var localCategory in category.Value)
                foreach (var obj in localCategory.Value)
                {
                    if(obj.discountCount > 0)
                            SpawnOnePlate().SetDataInPlate(obj, obj.Saved[0], 0);
                }
    }
    private void SpawnPlatesInIsNew()
    {

        foreach (var category in ContentManager.allDataDic)
        {
            foreach (var localCategory in category.Value)
                foreach (var obj in localCategory.Value)
                {
                    if (obj.isNew)
                        SpawnOnePlate().SetDataInPlate(obj, obj.Saved[0], 0);
                }
        }

    }
    private void SpawnPlatesInMostPopular()
    {
        foreach (var category in ContentManager.allDataDic)
            foreach (var localCategory in category.Value)
                foreach (var obj in localCategory.Value)
                {
                    if (obj.mostPopular)
                        SpawnOnePlate().SetDataInPlate(obj, obj.Saved[0], 0);
                }
    }

    private void SpawnPlatesInCart()
    {
        foreach (var category in ContentManager.allDataDic)
            foreach (var localCategory in category.Value)
                foreach (var obj in localCategory.Value)
                {
                    int i = 0;

                    foreach (var item in obj.Saved)
                    {
                        int j = 0;
                        foreach (var size in item.sizes)
                        {
                            if (size.inBagCount > 0)
                                SpawnOnePlate().SetDataInPlateCart(obj, item, i, j,this);
                            j++;
                        }
                        i++;
                    }
                }

    }
    private PlateData SpawnOnePlate()
    {
        var obj = Instantiate(_platePrefab, spawnTransform);
        _spawnedPlates.Add(obj);
        return obj;
    }
}