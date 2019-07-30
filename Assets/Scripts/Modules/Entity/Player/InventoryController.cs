using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Entity.Player
{
    public class InventoryController : MonoBehaviour
    {
        public UIInventory _uiInventory;
        public int _size;
        public List<Item> _items;

        public float _pickupDistance = 0.5f;

        private LayerMask _collectibleLayermask;

        void Start()
        {
            _items.Capacity = _size;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I)) {
                _uiInventory.gameObject.SetActive(!_uiInventory.gameObject.activeSelf);
            }
        }

        public void AddItem(Item item)
        {
            try
            {
                _items.Add(item);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InventoryFullException("Inventory is full.");
            }
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public bool ContainsItem(Item item)
        {
            return _items.Contains(item);
        }

        public int Size
        {
            get { return _size; }
            set
            {
                _size = value;
                _items.Capacity = _size;
            }
        }

        public class InventoryFullException : Exception
        {
            public InventoryFullException(string message) : base(message)
            {

            }
        }
    }
}