using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    protected RectTransform _rect;
    // Start is called before the first frame update

    protected Button _button;

    public virtual void Start()
    {
        _rect = GetComponent<RectTransform>();
    }

    public virtual void Initialize() {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual UIItem ReplaceItem(UIItem item) {
        //override in subclass

        return null;
    }

    public virtual bool IsItemInside(UIItem item) {
        if(item == null)
            return RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition);
        return RectTransformUtility.RectangleContainsScreenPoint(_rect, item.transform.position);
    }

    public virtual void AddItem(UIItem item) {
        //override in subclass
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
