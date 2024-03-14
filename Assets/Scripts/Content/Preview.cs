using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{
    public static Preview Instance;
    [SerializeField]
    public Obj properties { get; private set; }

    [SerializeField]
    private TextMeshProUGUI[] priceTMP;
    [SerializeField]
    private TextMeshProUGUI descriptionTMP;
    [SerializeField]
    private TextMeshProUGUI sizeTMP;
    private Saved saved;

    [SerializeField]
    private Button AddInCartButton;

    [SerializeField] private SpawnPlatesManager wishlistSpawnManager;
    private void OnDisable()
    {
        wishlistSpawnManager.OnEnable();
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _favouriteBttn.GetComponent<Button>().onClick.AddListener(
    () => ChangeSubscribeBttnColor(false));
        _unFavouriteBttn.GetComponent<Button>().onClick.AddListener(
            () => ChangeSubscribeBttnColor(true));
        gameObject.SetActive(false);

        AddInCartButton.onClick.AddListener(AddInCart);
    }
    [SerializeField] private GameObject discountObj;
    [SerializeField] private TextMeshProUGUI discountCountTMP;
    public void SetData(Obj properties)
    {
        choosedSize = "";
        AddInCartButton.interactable = false;
        this.properties = properties;
        SetSaved(properties.Saved[0]);
        SpawnAllColors();
        ChooseColor(saved.ColorName);
        foreach(var item in sizeButtons)
            item.EnableButton();
        if (saved.sizes.Count > 0)
        {
            inBagCount = saved.sizes[saved.sizes.Count - 1].inBagCount;
            inBagCountTMP.text = inBagCount.ToString();
            ChooseSize(saved.sizes[saved.sizes.Count - 1].size);
        }
        else 
            inBagCount = 0;

        if (inBagCount == 0)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;

        inBagCountTMP.text = inBagCount.ToString();
        ChangeSubscribeBttnColor(saved.favorite);

        descriptionTMP.text = properties.description;
        foreach(var obj in priceTMP)
            obj.text = ContentManager.instance.ChoosedValute.valute + (properties.price * ContentManager.instance.ChoosedValute.multiplier);

        if (discountObj != null)
        {
            discountObj.SetActive(properties.discountCount > 0);
            if (properties.discountCount > 0)
            {
                discountCountTMP.text = "-" + properties.discountCount.ToString() + "%";
                discountObj.GetComponent<TextMeshProUGUI>().text = ContentManager.instance.ChoosedValute.valute + (properties.price * ContentManager.instance.ChoosedValute.multiplier);
                foreach (var obj in priceTMP)
                    obj.text = ContentManager.instance.ChoosedValute.valute + (properties.price - (((float)properties.discountCount / 100 * properties.price) * ContentManager.instance.ChoosedValute.multiplier)).ToString();
            }
        }
    }
    [SerializeField]
    private TextMeshProUGUI inBagCountTMP;

    public void SetSaved(Saved saved)
    {
        this.saved = saved;
    }
    private string choosedSize;
    private string choosedColor;
    private int inBagCount;

    [SerializeField] private Button minusButton;
    public void PlusMinusInBag(int i)
    {
        inBagCount += i;
        if(inBagCount == 0)
            minusButton.interactable = false;
        else
            minusButton.interactable = true;

        inBagCountTMP.text = inBagCount.ToString();
        if(choosedSize != "" & inBagCount > 0)
            AddInCartButton.interactable = true;
        else
            AddInCartButton.interactable = false;

    }
    public void ChooseSize(string size)
    {
        foreach (var button in sizeButtons)
        {
            if(button.size == size) button.DisableButton();
            else button.EnableButton();
        }
        choosedSize = size;
        sizeTMP.text = choosedSize.ToString();
        if(inBagCount > 0)
            AddInCartButton.interactable = true;
    }
    [SerializeField] private LoadPlateImage loadPlateImageSmall;
    public void ChooseColor(string color)
    {
        foreach (var button in colorButtons)
        {
            if (button.color == color) button.DisableButton();
            else button.EnableButton();
        }
        GetComponent<LoadPlateImage>().LoadPlateImageInRawImage(color, color);
        loadPlateImageSmall.LoadPlateImageInRawImage(color, color);
        choosedColor = color;
    }
    public void AddInCart()
    {
        bool isHaveThisColor = false;
        int j = 0;
        for (int i = 0; i < properties.Saved.Count; i++)
        {
            if (properties.Saved[i].ColorName == choosedColor)
            {
                isHaveThisColor = true;
                j = i;
            }
        }
        if (!isHaveThisColor)
        {
            properties.Saved.Add(new Saved { ColorName = choosedColor,sizes = new List<Size>()});
            SetSize(properties.Saved[properties.Saved.Count - 1]);
        }
        else SetSize(properties.Saved[j]);
        ContentManager.instance.StartSaveCoroutine();
        BottomPanel.instance.ChangeWindow(2);
    }

    private void SetSize(Saved saved)
    {
        bool isHaveThisSize = false;
        int j = 0;
        Debug.Log(saved);
        Debug.Log(saved.sizes.Count);
        for (int i = 0; i < saved.sizes.Count; i++)
        {
            if (saved.sizes[i].size == choosedSize)
            {
                isHaveThisSize = true;
                j = i;
            }
        }
        if (!isHaveThisSize)
        {
            saved.sizes.Add(new Size { size = choosedSize});
            saved.sizes[saved.sizes.Count - 1].inBagCount = inBagCount;
        }
        else
        {
            saved.sizes[j].inBagCount = inBagCount;
        }
    }
    [SerializeField]
    private GameObject _favouriteBttn;
    [SerializeField]
    private GameObject _unFavouriteBttn;
    public void ChangeSubscribeBttnColor(bool isFolowed)
    {
        if (isFolowed)
        {
            _favouriteBttn.SetActive(true);
            _unFavouriteBttn.SetActive(false);
        }
        else
        {
            _favouriteBttn.SetActive(false);
            _unFavouriteBttn.SetActive(true);
        }
        saved.favorite = isFolowed;
        ContentManager.instance.StartSaveCoroutine();

    }
    [SerializeField] private SizeButton[] sizeButtons;

    private List<ColorButton> colorButtons = new List<ColorButton>();
    [SerializeField] private ColorButton colorPrefab;
    [SerializeField] private Transform colorSpawnPlace;
    private void SpawnAllColors()
    {
        SpawnAllVariations();
        foreach (var button in colorButtons)
        {
            Destroy(button.gameObject);
        }
        colorButtons.Clear();
        foreach(var item in properties.imageLinks)
        {
            var obj = Instantiate(colorPrefab, colorSpawnPlace);
            obj.SetData(item);
            colorButtons.Add(obj);
        }
    }
    private List<GameObject> variations = new List<GameObject>();
    [SerializeField]private Transform variationSpawnPlace;
    private void SpawnAllVariations()
    {
        foreach (var variation in variations)
        {
            Destroy(variation);
        }
        variations.Clear();
        foreach (var item in properties.imageLinks)
        {
            var obj = new GameObject();
            obj.transform.parent = variationSpawnPlace;
            obj.transform.localScale = Vector3.one;
            //var obj = Instantiate(new GameObject(), variationSpawnPlace);
            var img = obj.AddComponent<RawImage>();
            img.texture = ImagesHolder.Instance.image[Convert.ToInt16(item)];
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(225, 225);
            variations.Add(obj);
        }
    }
}
