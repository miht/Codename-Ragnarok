using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI
{
    public class UIInventory : MonoBehaviour
    {
        public UIItem _uiItemPrefab;

        public UIContainer[] _uiContainers;

        public UIBackpack _uiBackpack;

        private UIItem _selectedItem;

        private bool b_isItemOutside = false;

        private RectTransform _rect;

        void Start()
        {
            _rect = GetComponent<RectTransform>();
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
                Destroy(_selectedItem.gameObject);
                _selectedItem = null;
                b_isItemOutside = false;
                return;
            }

            foreach (UIContainer container in _uiContainers)
            {
                if (!container.IsItemInside(_selectedItem))
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
                    isInSlot |= slot.IsItemInside(_selectedItem);
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

        public void AddItem(Item item, Action<Item> onDrop)
        {
            //TODO: If equippable AND if slot is currently empty, equip straight away. Otherwise, add in bag
            UIItem uiItem = Instantiate<UIItem>(_uiItemPrefab, transform);
            uiItem.Initialize(item, () => {
                onDrop(item);
            });
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
}

