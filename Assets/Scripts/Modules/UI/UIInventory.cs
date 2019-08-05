using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public UIItem _uiItemPrefab;

    public UIContainer[] _uiContainers;

    public UIBackpack _uiBackpack;

    private UIItem _selectedItem;
    
    private RectTransform _rect;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        
        foreach (UIContainer container in _uiContainers) {
            container.Initialize(() => {
                if(_selectedItem != null) {
                    if (!_selectedItem.IsValid()) {
                        return;
                    }
                }
                _selectedItem = container.ReplaceItem(_selectedItem);
            });
        }
    }

    void Awake()
    {

    }

    public void OnClick() {
    }

    void Update() {
        if(_selectedItem != null) {
            bool isInSlot = false;
            foreach(UIContainer slot in _uiContainers) {
                isInSlot |= slot.IsItemInside(_selectedItem);
            }

            int numberOfIntersections = 0;
            foreach(UIItem uiItem in _uiBackpack.GetItems()) {
                numberOfIntersections += _selectedItem.Intersects(uiItem)? 1 : 0;
            }

            _selectedItem.SetValid(isInSlot && numberOfIntersections <= 1);
        }
    }

    public void AddItem(Item item, Action<Item> onDrop) {
        //TODO: If equippable AND if slot is currently empty, equip straight away. Otherwise, add in bag
        UIItem uiItem = Instantiate<UIItem>(_uiItemPrefab, transform);
        uiItem.Initialize(item._uiDimensions, item._uisprite, item.Rarety);
        _uiBackpack.AddItem(uiItem);
    }

    public void AddItemToBackpack(Item item)
    {
        // //TODO: If equippable AND if slot is currently empty, equip straight away. Otherwise, add in bag
        // bool canAddToBackpack = _uiBackpack.CanAdd(item);
        // if(canAddToBackpack) {
        //     _uiBackpack.AddItem(item);
        // }
    }
}
