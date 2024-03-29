﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Modules.Entity.Player;

namespace Modules.UI
{
    public class UIInventory : MonoBehaviour
    {
        public UIItem _uiItemPrefab;

        private UIContainer[] _uiContainers;

        public UIBackpack _uiBackpack;

        private UIItem _selectedItem;

        private bool b_isItemOutside = false;

        private RectTransform _rect;

        void Start()
        {
            _rect = GetComponent<RectTransform>();

            _uiContainers = GetComponentsInChildren<UIContainer>();
            foreach (UIContainer container in _uiContainers)
            {
                container.Initialize();
            }
        }

        void Awake()
        {

        }

        public void OnClick()
        {
            if (b_isItemOutside)
            {
                _selectedItem.GetDropAction()();
                _selectedItem.SetDragged(false);
                Destroy(_selectedItem.gameObject);
                _selectedItem = null;
                b_isItemOutside = false;
                return;
            }

            foreach (UIContainer container in _uiContainers)
            {
                if (!container.CanHostItem(_selectedItem))
                    continue;

                if (_selectedItem != null)
                {
                    if (!_selectedItem.IsValid())
                    {
                        return;
                    }
                }
                _selectedItem = container.ReplaceItem(_selectedItem);
                break;
            }
        }

        void Update()
        {
            if (_selectedItem != null)
            {
                bool isInSlot = false;
                foreach (UIContainer slot in _uiContainers)
                {
                    isInSlot |= slot.CanHostItem(_selectedItem);
                }

                b_isItemOutside = !RectTransformUtility.RectangleContainsScreenPoint(_rect, _selectedItem.transform.position);

                int numberOfIntersections = 0;
                foreach (UIItem uiItem in _uiBackpack.GetItems())
                {
                    numberOfIntersections += _selectedItem.Intersects(uiItem) ? 1 : 0;
                }
                _selectedItem.SetValid((isInSlot && numberOfIntersections <= 1) || b_isItemOutside);
            }

            if(Input.GetMouseButtonDown(0)) {
                OnClick();
            }
        }

        public void AddItem(Item item, Action OnGrab, Action OnRelease, Action<Item> onDrop)
        {
            //TODO: If equippable AND if slot is currently empty, equip straight away. Otherwise, add in bag
            Vector2 position;
            bool foundPosition = _uiBackpack.FindAvailablePosition(item._uiDimensions.x, item._uiDimensions.y, out position);
            if(foundPosition) {
                UIItem uiItem = Instantiate<UIItem>(_uiItemPrefab, transform);
                uiItem.Initialize(item, transform.localScale, OnGrab, OnRelease, () =>
                {
                    onDrop(item);
                    TooltipController.GetInstance().HideTooltip();
                });
                uiItem.SetDragged(false);
                _uiBackpack.AddItem(uiItem, position);
            } else {
                throw new InventoryController.InventoryFullException("Inventory is full.");
            }
            
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
}

