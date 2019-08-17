using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    protected RectTransform _rect;
    public Vector2 _pivot;
    // Start is called before the first frame update

    protected Button _button;

    public virtual void Start()
    {
    }

    public virtual void Initialize() {
        _rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual UIItem ReplaceItem(UIItem item) {
        //override in subclass

        return null;
    }

    public virtual bool CanHostItem(UIItem item) {
        return RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition);
    }

    public virtual void AddItem(UIItem item) {
        //override in subclass
        item.GetRectTransform().pivot = _pivot;
    }

    public virtual UIItem RemoveItem() {
       //override in subclass
       return null;
    }

    public RectTransform GetRectTransform() {
        return _rect;
    }

    public virtual UIItem GetItem()
    {
        return null;
    }

    public virtual bool HasItem()
    {
        return false;
    }
}
