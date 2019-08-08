using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackpack : UIContainer
{
    public Vector2Int _dimensions;
    private Vector2 _cellPixelDimensions;

    private bool[] _occupiedSlots;
    private List<UIItem> _items;
    public override void Start() {
        base.Start();
        _occupiedSlots = new bool[_dimensions.x * _dimensions.y];
        _items = new List<UIItem>();
    }

    public override void Initialize() {
        base.Initialize();

        _cellPixelDimensions = new Vector2(_rect.rect.width / _dimensions.x, _rect.rect.height / _dimensions.y);
    }

    public override UIItem ReplaceItem(UIItem item) {
        base.ReplaceItem(item);
        UIItem replaceItem = null;
        foreach(UIItem uiItem in _items) {
            if(item == null) {
                if(RectTransformUtility.RectangleContainsScreenPoint(uiItem.GetRectTransform(), Input.mousePosition)) {
                    replaceItem = uiItem;
                    break;
                }
            }
            else {
                if(item.Intersects(uiItem)) {
                    replaceItem = uiItem;
                    break;
                }
            }
        }
        RemoveItem(replaceItem);
        AddItem(item);
        return replaceItem;
    }

    public override void AddItem(UIItem item)
    {
        if(item == null)
            return;

        base.AddItem(item);

        bool isDragging = item.Dragging;
        item.SetDragged(false);
        item.SetValid(true);
        item.transform.SetParent(transform);

        _items.Add(item);

        if(isDragging) {
            item.transform.localPosition = CalculatePosition(item.transform.localPosition);
        }
        else {
            Vector3 foundPosition;
            if (FindAvailablePosition(item, out foundPosition))
            {
                item.transform.localPosition = foundPosition;
            }
        }
        FillSlots(item, true);
    }

    public override UIItem GetItem()
    {
        foreach (UIItem uiItem in _items)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(uiItem.GetRectTransform(), Input.mousePosition))
            {
                return uiItem;
            }
        }
        return null;
    }
        
    private void RemoveItem(UIItem item) {
        if(!_items.Contains(item))
            return;

        FillSlots(item, false);

        _items.Remove(item);
        item.SetDragged(true);
        item.transform.SetParent(item.transform.parent.parent);
    }

    private bool SlotsAvailable(UIItem item, int x, int y) {
        bool available = true;
        Vector2 itemDimensions = item.GetDimensions();

        if(_occupiedSlots[GetIndex(x, y)])
            return false;

        for (int i = 0; i < itemDimensions.x; i++)
        {
            for (int j = 0; j < itemDimensions.y; j++)
            {
                available &= !_occupiedSlots[GetIndex(x + i, y + j)];
            }
        }

        return available;
    }

    private void FillSlots(UIItem item, bool value) {
        Vector2 itemDimensions = item.GetDimensions();

        int posX = Mathf.RoundToInt(item.transform.localPosition.x / _cellPixelDimensions.x + _dimensions.x / 2f - itemDimensions.x / 2f);
        int posY = Mathf.RoundToInt(-(item.transform.localPosition.y / _cellPixelDimensions.y - _dimensions.y / 2f + itemDimensions.y / 2f));

        for (int x = 0; x < itemDimensions.x; x++)
        {
            for (int y = 0; y < itemDimensions.y; y++)
            {
                _occupiedSlots[GetIndex(posX + x, posY + y)] = value;
            }
        }
    }

    private bool FindAvailablePosition(UIItem item, out Vector3 position) {
        int itemWidth = item.GetDimensions().x;
        int itemHeight = item.GetDimensions().y;
        for(int bX = 0; bX < _dimensions.x - itemWidth; bX ++) {
            for (int bY = 0; bY < _dimensions.y - itemHeight; bY++) {
                if(SlotsAvailable(item, bX, bY)) {
                    position = new Vector3(_rect.rect.xMin + bX * _cellPixelDimensions.x + (itemWidth * _cellPixelDimensions.x)/2f, 
                    _rect.rect.yMax - bY * _cellPixelDimensions.y - (itemHeight * _cellPixelDimensions.y)/2f, item.transform.position.z);
                    return true;
                }
            }
        }
        position = item.transform.position;
        return false;
    }

    private int GetIndex(int x, int y) {
        return x * _dimensions.y + y;
    }

    private Vector3 CalculatePosition(Vector3 position) {
        float x = Mathf.Round((position.x - _cellPixelDimensions.x / 2)/ _cellPixelDimensions.x) * _cellPixelDimensions.x + _cellPixelDimensions.x / 2;
        float y = Mathf.Round((position.y - _cellPixelDimensions.y / 2) / _cellPixelDimensions.y) * _cellPixelDimensions.y + _cellPixelDimensions.y / 2f;

        return new Vector3(x, y, position.z);
    }

    public override bool IsItemInside(UIItem item) {
        if(item == null)
            return RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition);
        return ContainsRect(_rect, item.GetRectTransform(), item._colliderMargins);
    }

    public List<UIItem> GetItems() {
        return _items;
    }

    private int GetIndexForPosition(Vector2 position)
    {
        Debug.Log(position);
        Debug.Log(_rect.position);

        return 0;
    }


    public bool ContainsRect(RectTransform major, RectTransform minor, Vector2 margins)
    {
        Vector3[] majorCorners = new Vector3[4];
        Vector3[] minorCorners = new Vector3[4];
        major.GetWorldCorners(majorCorners);
        minor.GetWorldCorners(minorCorners);
        return (majorCorners[0].x - margins.x / 2f < minorCorners[0].x &&
            majorCorners[0].y - margins.y / 2f < minorCorners[0].y &&
            majorCorners[2].x + margins.x / 2f > minorCorners[2].x &&
            majorCorners[2].y + margins.y / 2f > minorCorners[2].y);
    }
}
