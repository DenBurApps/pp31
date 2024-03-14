using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas instance;
    public static float standartCanvasHeight = 2436f;
    public static float ScaleModifier;

    [SerializeField] private GameObject[] activateThis;
    private void Start()
    {

        if (instance != null)
            Destroy(gameObject);
        instance = this;
        var height = GetComponent<RectTransform>().rect.height;
        ScaleModifier = height / standartCanvasHeight;
        Debug.Log("ScaleModifier - " + ScaleModifier);
        Debug.Log("width - " + height);
        Debug.Log("standartCanvasWidth - " + standartCanvasHeight);
        foreach (var item in activateThis)
            item.SetActive(true);
    }
}
