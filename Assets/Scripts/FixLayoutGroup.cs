using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixLayoutGroup : MonoBehaviour
{
    private RectTransform layout;
    private void Awake()
    {
        layout = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(layout);
    }
}
