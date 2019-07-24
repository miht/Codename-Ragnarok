using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Modules.Entity.Player {
    public class PlayerController : MonoBehaviour
    {
        public float _speed = 5f;
        public float _acceleration = 1f;
        private NavMeshAgent _navMeshAgent;
        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _speed;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.acceleration = _acceleration;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButton(0)) {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, 45f, 1 << 9)) {
                    Transform objectHit = hit.transform;
                    _navMeshAgent.SetDestination(hit.point);
                }
            }
        }

        void LateUpdate() {
            if(_navMeshAgent.velocity.magnitude > Mathf.Epsilon) {
                transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);    
            }
        }
    }
}

