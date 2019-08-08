using System;
using System.Collections.Generic;
using UnityEngine;
using Modules.UI;

namespace Modules.Entity.Player
{
    public class InventoryController : MonoBehaviour
    {
        public ObtainableObject _obtainableObjectPrefab;

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
                _uiInventory.AddItem(item, RemoveItem);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InventoryFullException("Inventory is full.");
            }
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            Vector3 charPos = transform.position;
            Vector3 dropPoint = transform.position;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 45f, LayerMask.GetMask("Terrain")))
            {
                Debug.Log("Dropping on mouse!");
                charPos.y = hit.point.y;
                dropPoint = charPos + (hit.point - charPos).normalized;
            }
            else {
                dropPoint = charPos + transform.forward;
                Debug.Log("Dropping forward!");
                charPos.y = hit.point.y;
                dropPoint = charPos + transform.forward;
            }
            dropPoint.y = 0;

            ObtainableObject o = Instantiate(_obtainableObjectPrefab, dropPoint, Quaternion.identity);
            o._item = item;
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