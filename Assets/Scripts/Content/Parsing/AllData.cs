using System;
using System.Collections.Generic;

[Serializable]
public class Country
{
    public string name;
    public string code;
}
[Serializable]
public class Countrys
{
    public List<Country> Country;
}
[Serializable]
public class SettingsInfo
{
    public string choosedCountry;

    public string address;
    public string town;
    public string postcode;
}

[Serializable]
public class Valute
{
    public string valute;
    public float multiplier;
    public string valuteFullName;
}
[Serializable]
public class Root
{
    public Valute valute;
    public SettingsInfo settingsInfo;
    public List<Category> category;
}
[Serializable]
public class Category
{
    public string categoryName;
    public List<LocalCategory> localCategory;
}
[Serializable]

public class LocalCategory
{
    public string localCategoryname;
    public List<Obj> obj;
}
[Serializable]
public class Obj
{
    public int ID;
    public int price;
    public int discountCount;
    public string description;
    public string name;
    public bool isNew;
    public bool mostPopular;
    public List<string> imageLinks;

    public List<Saved> Saved;
}
[Serializable]
public class Saved
{
    public bool favorite;

    public string ColorName;
    public List<Size> sizes;
}
[Serializable]
public class Size
{
    public string size;
    public int inBagCount;

}