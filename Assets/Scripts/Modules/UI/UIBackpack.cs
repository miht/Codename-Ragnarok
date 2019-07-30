using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackpack : MonoBehaviour
{
    private RectTransform _rect;
    public Vector2 _dimensions;
    public int _cellSize = 64;

    void Start() {
        _rect = GetComponent<RectTransform>();
    }
    
    public void AddItem(GameObject gameObject) {
        Instantiate(gameObject, transform);
    }
}
