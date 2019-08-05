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

    public virtual void Initialize(Action onClick) {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => {
            onClick();
        });
    }

    // Update is called once per frame
    void Update()
    {
        UIUtilities.DebugRect(_rect.rect, Color.green);
    }

    public virtual UIItem ReplaceItem(UIItem item) {
        //override in subclass

        return null;
    }

    public virtual bool IsItemInside(UIItem item) {
        return RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition);
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
