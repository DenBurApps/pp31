using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlateData : MonoBehaviour
{
    public Obj Properties;

    [SerializeField]
    public RawImage Picture;
    [SerializeField]
    private TextMeshProUGUI NameTMP;
    [SerializeField]
    private GameObject discountObj;
    [SerializeField]
    private LoadPlateImage loadPlateImage;

    [SerializeField]
    private TextMeshProUGUI priceTMP;
    [SerializeField]
    private TextMeshProUGUI descriptionTMP;
    [SerializeField]
    private Button plateButton;

    private Saved savedLocal;
    public void SetDataInPlate(Obj properties, Saved saved,int savedIndex)
    {
        this.Properties = properties;
        savedLocal = saved;

        if (priceTMP != null)
            priceTMP.text = ContentManager.instance.ChoosedValute.valute + (properties.price * ContentManager.instance.ChoosedValute.multiplier);

        if (discountObj != null)
        {
            discountObj.SetActive(properties.discountCount > 0);
            if (properties.discountCount > 0)
            {
                discountObj.GetComponent<TextMeshProUGUI>().text = ContentManager.instance.ChoosedValute.valute + (Properties.price * ContentManager.instance.ChoosedValute.multiplier).ToString();
                priceTMP.text = ContentManager.instance.ChoosedValute.valute + ((Properties.price - ((float)Properties.discountCount / 100 * Properties.price)) * ContentManager.instance.ChoosedValute.multiplier).ToString();
            }
        }
        if (NameTMP != null)
            NameTMP.text = properties.name;


        var imageLinks = properties.imageLinks;
        if (savedLocal.ColorName != null && savedLocal.ColorName != "")
            loadPlateImage.LoadPlateImageInRawImage(savedLocal.ColorName, savedLocal.ColorName);

        if (plateButton != null)
            plateButton.onClick.AddListener(() => {
                Preview.Instance.gameObject.SetActive(true);
                Preview.Instance.SetData(properties);
            });

        if (descriptionTMP != null)
            descriptionTMP.text = properties.description;
        this.savedIndex = savedIndex;
    }

    public void SetDataInPlateWishlist(Obj properties, Saved saved, int savedIndex, SpawnPlatesManager spawnPlatesManager)
    {
        SetDataInPlate(properties, saved, savedIndex);
        _unFavouriteBttn.GetComponent<Button>().onClick.AddListener(
    () => { ChangeSubscribeBttnColor(false);
        spawnPlatesManager.OnEnable();

    });
    }
    public void SetDataInPlateCart(Obj properties, Saved saved, int savedIndex,int sizeIndex, SpawnPlatesManager spawnPlatesManager)
    {
        SetDataInPlate(properties, saved, savedIndex);
        _unFavouriteBttn.GetComponent<Button>().onClick.AddListener(
    () => { 
        saved.sizes[sizeIndex].inBagCount = 0;
        ContentManager.instance.StartSaveCoroutine();
        //Destroy(gameObject); 
        spawnPlatesManager.OnEnable();
    });
        this.sizeIndex = sizeIndex;
        InBagCountTMP.text = saved.sizes[sizeIndex].inBagCount.ToString();
        sizeTMP.text = "Size " + saved.sizes[sizeIndex].size.ToString();
    }
    [SerializeField] private TextMeshProUGUI sizeTMP;
    [SerializeField] private Button minusButton;
    [SerializeField] private TextMeshProUGUI InBagCountTMP;
    private int sizeIndex;
    public void PlusMinusInBag(int i)
    {
        savedLocal.sizes[sizeIndex].inBagCount += i;
        if (savedLocal.sizes[sizeIndex].inBagCount == 0)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;

        InBagCountTMP.text = savedLocal.sizes[sizeIndex].inBagCount.ToString();
        ContentManager.instance.StartSaveCoroutine();
        CalculateCost.Instance.Calculate();
    }

    public int ReturnInBagCount()
    {
        return savedLocal.sizes[sizeIndex].inBagCount;
    }
    [SerializeField]
    private GameObject _unFavouriteBttn;
    public int savedIndex;
    public void ChangeSubscribeBttnColor(bool isFolowed)
    {
        Properties.Saved[savedIndex].favorite = isFolowed;

        if (!isFolowed)
        {
            gameObject.SetActive(false);
        }
        ContentManager.instance.StartSaveCoroutine();
    }
}
