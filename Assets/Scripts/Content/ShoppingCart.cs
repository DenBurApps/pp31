using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    public static ShoppingCart Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
