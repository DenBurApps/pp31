using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleProportional : MonoBehaviour
{
    private void Start()
    {
        var s = MainCanvas.ScaleModifier;
        GetComponent<RectTransform>().localScale = new Vector3(s,s,s);
    }
}
