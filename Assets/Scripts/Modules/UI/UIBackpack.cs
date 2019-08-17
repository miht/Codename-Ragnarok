using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Modules.Debugging;

public class UIBackpack : UIContainer
{
    public GameObject _backpackSlotPrefab;

    public Vector2Int _dimensions;
    private Vector2 _cellPixelDimensions;

    private UIBackpackSlot[,] _backpackSlots;
    public bool highLightSlots = false;
    private List<UIItem> _items;
    public override void Start() {
        base.Start();
        // _backpackSlots = new bool[_dimensions.x * _dimensions.y];
        _backpackSlots = GenerateSlots();

        _items = new List<UIItem>();
    }

    public UIBackpackSlot[,] GenerateSlots() {
        GameObject slotView = new GameObject();
        slotView.transform.SetParent(transform);

        RectTransform rectTransform = slotView.AddComponent<RectTransform>();
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.one;

        GridLayoutGroup gridLayout = slotView.AddComponent<GridLayoutGroup>();
        gridLayout.startAxis = GridLayoutGroup.Axis.Vertical;
        slotView.transform.localPosition = new Vector2(-_rect.rect.width / 2f, _rect.rect.height / 2f);
        slotView.transform.localScale = Vector3.one;

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = _dimensions.x;

        gridLayout.cellSize = new Vector2(_rect.rect.width / _dimensions.x, _rect.rect.height / _dimensions.y);

        UIBackpackSlot[,] slots = new UIBackpackSlot[_dimensions.x, _dimensions.y];
        for(int x = 0; x < slots.GetLength(0); x++) {
            for(int y = 0; y < slots.GetLength(1); y++) {
            Image image = Instantiate(_backpackSlotPrefab, slotView.transform).GetComponent<Image>();

            slots[x, y] = new UIBackpackSlot(image, false);
            }
        }

        return slots;
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

    public void AddItem(UIItem item, Vector2 position) {
        base.AddItem(item);
        item.SetValid(true);
        item.transform.SetParent(transform);
        item.AnchoredPosition = position;
        _items.Add(item);

        Vector2Int index = GetIndexForPosition(position);
        FillSlots(index.x, index.y, item.GetDimensions().x, item.GetDimensions().y, true);
    }

    public override void AddItem(UIItem item)
    {
        if (item == null)
            return;

        base.AddItem(item);

        bool isDragging = item.Dragging;
        item.SetDragged(false);
        item.SetValid(true);
        item.transform.SetParent(transform);

        _items.Add(item);

        item.GetRectTransform().anchoredPosition = CalculatePosition(item.GetRectTransform().anchoredPosition);
        Vector2Int index = GetIndexForPosition(item.GetRectTransform().anchoredPosition);
        FillSlots(index.x, index.y, item.GetDimensions().x, item.GetDimensions().y, true);
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

        Vector2Int pos = GetIndexForPosition(item.AnchoredPosition);
        FillSlots(pos.x, pos.y, item.GetDimensions().x, item.GetDimensions().y, false);

        _items.Remove(item);
        item.SetDragged(true);
        item.transform.SetParent(item.transform.parent.parent);
    }

    private bool SlotsAvailable(int x, int y, int width, int height) {
        bool available = true;

        if(_backpackSlots[x, y].IsOccupied())
            return false;

            if(x > _dimensions.x || x < 0 || y > _dimensions.y || y < 0)
                return false;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                available &= !_backpackSlots[x + i, y + j].IsOccupied();
            }
        }
        return available;
    }

    private void FillSlots(int x, int y, int width, int height, bool value) {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _backpackSlots[x + i, y + j].SetOccupied(value);
            }
        }
    }

    public bool FindAvailablePosition(int width, int height, out Vector2 position) {
        for(int bX = 0; bX <= _dimensions.x - width; bX ++) {
            for (int bY = 0; bY <= _dimensions.y - height; bY++) {
                if(SlotsAvailable(bX, bY, width, height)) {
                    position = GetPositionForIndex(bX, bY);
                    return true;
                }
            }
        }
        position = Vector2.zero;
        return false;
    }

    private int GetIndex(int x, int y) {
        return x * _dimensions.y + y;
    }

    private Vector2 CalculatePosition(Vector3 position) {
        float x = Mathf.Round(position.x / _cellPixelDimensions.x) * _cellPixelDimensions.x;
        float y = Mathf.Round(position.y / _cellPixelDimensions.y) * _cellPixelDimensions.y;

        return new Vector2(x, y);
    }

    public override bool CanHostItem(UIItem item) {
        if(item == null)
            return RectTransformUtility.RectangleContainsScreenPoint(_rect, Input.mousePosition);
        return ContainsRect(_rect, item.GetRectTransform(), item._colliderMargins);
    }

    public List<UIItem> GetItems() {
        return _items;
    }

    private Vector2Int GetIndexForPosition(Vector2 position)
    {
        int posX = Mathf.RoundToInt(position.x / _cellPixelDimensions.x);
        int posY = -Mathf.RoundToInt(position.y / _cellPixelDimensions.y);

        return new Vector2Int(posX, posY);
    }


    private Vector2 GetPositionForIndex(int x, int y)
    {
        return new Vector2(x * _cellPixelDimensions.x, - y*_cellPixelDimensions.y);
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

public struct UIBackpackSlot {
    Image _image;
    bool _occupied;

    public UIBackpackSlot(Image image, bool occupied) {
        _image = image;
        _occupied = occupied;  
    }
    public void SetOccupied(bool value) {
        _occupied = value;
        _image.enabled = !_occupied;
    }

    public bool IsOccupied() {
        return _occupied;
    }
}
