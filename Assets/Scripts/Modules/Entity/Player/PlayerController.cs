using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

using Modules.UI;

namespace Modules.Entity.Player {
    public class PlayerController : Controller
    {

        public float _speed = 5f;
        public float _acceleration = 1f;
        public float _movementDistanceThreshold = 0.5f;
        private PlayerNavmeshAgent _agent;

        private InventoryController _inventoryController;

        private TargetableObject _target;

        private UIView _uiCanvas;

        private bool _isMouseValid;

        // Start is called before the first frame update
        void Start()
        {
            _uiCanvas = GameObject.FindWithTag("Canvas").GetComponent<UIView>();

            _inventoryController = GetComponent<InventoryController>();

            _agent = gameObject.AddComponent<PlayerNavmeshAgent>();
            _agent.Setup();
            _agent.SetSpeed(_speed);
            _agent.SetAcceleration(_acceleration);
            _agent.SetMovementDistanceThreshold(_movementDistanceThreshold);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButton(0)) { 
                if(Input.GetMouseButtonDown(0)) {
                    _isMouseValid = !IsMouseOverUI();
                }

                if(_isMouseValid) {

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 45f, LayerMask.GetMask("Terrain")))
                    {
                        Transform objectHit = hit.transform;
                        _agent.SetDestination(hit.point);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && !_inventoryController.IsHoldingItem())
            {
                RaycastHit targetableHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out targetableHit, 45f, LayerMask.GetMask("TargetableObject")))
                {
                    Transform targetableObjectHit = targetableHit.transform;
                    TargetableObject tarObj = targetableObjectHit.GetComponent<TargetableObject>();
                    _agent.SetTarget(tarObj, (tar) => Interact(tar));
                }
            }
        }

        private void DropItem(Item item) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 45f, LayerMask.GetMask("Terrain")))
            {
                Transform objectHit = hit.transform;
                _agent.SetDestination(hit.point);
            }
        }
        
        private void Interact(TargetableObject obj) {
            switch(obj.TargetableObjectType) {
                case TargetableObject.TargetableObjectTypes.Obtainable:
                    try {
                        ObtainableObject obtObj = obj.GetComponent<ObtainableObject>();
                        _inventoryController.AddItem(obtObj.GetItem());
                        Destroy(obj.gameObject);
                    }
                    catch (InventoryController.InventoryFullException e) {
                        //Trigger object animation here
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

