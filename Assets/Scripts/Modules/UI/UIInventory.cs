using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public UIBackpack _uiBackpack;
    
    private RectTransform _rect;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    void Awake()
    {

    }
}
