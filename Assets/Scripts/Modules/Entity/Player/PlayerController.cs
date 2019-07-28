using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Entity.Player {
    public class PlayerController : MonoBehaviour
    {
        public float _speed = 5f;
        public float _acceleration = 1f;
        private float _minimumMovementDistance = 0.5f;
        private NavMeshAgent _navMeshAgent;

        private InventoryController _inventoryController;

        private TargetableObject _target;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _speed;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.acceleration = _acceleration;
            _minimumMovementDistance = _navMeshAgent.radius;

            _inventoryController = GetComponent<InventoryController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButton(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 45f, LayerMask.GetMask("Terrain"))) {
                    Transform objectHit = hit.transform;
                    
                    Vector3 distVector = hit.point - transform.position;
                    distVector.y = 0;
                    float distance = Vector3.Magnitude(distVector);
                    if(distance >= _minimumMovementDistance) {
                        _navMeshAgent.isStopped = false;
                        _navMeshAgent.SetDestination(hit.point);
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit targetableHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out targetableHit, 45f, LayerMask.GetMask("TargetableObject")))
                {
                    Transform targetableObjectHit = targetableHit.transform;
                    TargetableObject tarObj = targetableObjectHit.GetComponent<TargetableObject>();
                    if (tarObj != null)
                    {
                        _navMeshAgent.isStopped = false;
                        Debug.Log("PlayerController: Targetable object hit");
                        _navMeshAgent.SetDestination(targetableObjectHit.position);
                        _target = tarObj;
                    }
                }
            }
        }

        void LateUpdate() {
            if(_navMeshAgent.velocity.magnitude > Mathf.Epsilon) {
                transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);    
            }
        }

        void OnTriggerEnter(Collider other) {
            Debug.Log(other.gameObject.name);
            if (_target != null && _target == other.gameObject.GetComponent<TargetableObject>())
            {
                _navMeshAgent.isStopped = true;
                _target = null;
                Debug.Log("PlayerController: Target reached.");

                //Check if it was an item that we collided with
                ObtainableObject obtObject = other.GetComponent<ObtainableObject>();
                if(obtObject != null) {
                    try {
                        _inventoryController.AddItem(obtObject.GetItem());
                        Destroy(other.gameObject);
                    }
                    catch (InventoryController.InventoryFullException) {
                        //Animate the object or do something here
                    }
                }
            }
        }
    }
}

