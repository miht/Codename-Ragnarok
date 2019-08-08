using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerNavmeshAgent : MonoBehaviour
{
    private NavMeshAgent _agent;

    private TargetableObject _target;
    private float _movementDistanceThreshold;
    private Action<TargetableObject> _targetAction;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Setup()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void SetSpeed(float speed) {
        _agent.speed = speed;
    }
    public void SetAcceleration(float acceleration) {
        _agent.acceleration = acceleration;
    }
    public void SetMovementDistanceThreshold(float movementDistanceThreshold) {
        _movementDistanceThreshold = movementDistanceThreshold;
    }

    public void SetDestination(Vector3 destination)
    {
        Vector3 distVector = destination - transform.position;
        distVector.y = 0;
        float distance = Vector3.Magnitude(distVector);

        if (distance > _movementDistanceThreshold)
        {
            _agent.isStopped = false;
            _agent.SetDestination(destination);
        }
    }

    public void SetTarget(TargetableObject target, Action<TargetableObject> targetAction = null)
    {
        if(target == null)
            return;
        _agent.isStopped = false;
        _agent.SetDestination(target.transform.position);
        _targetAction = targetAction;
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance < Mathf.Epsilon)
        {
            Stop();
        }
    }

    void LateUpdate()
    {
        if (_agent.velocity.magnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
        }
    }

    public void Stop()
    {
        _agent.isStopped = true;
        _targetAction = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (_target != null && _target == other.gameObject.GetComponent<TargetableObject>())
        {
            if (_targetAction != null)
            {
                _targetAction(_target);
            }

            Stop();




            // //Check if it was an item that we collided with
            // ObtainableObject obtObject = other.GetComponent<ObtainableObject>();
            // if (obtObject != null)
            // {
            //     try
            //     {
            //         _inventoryController.AddItem(obtObject.GetItem());
            //         Destroy(other.gameObject);
            //     }
            //     catch (InventoryController.InventoryFullException)
            //     {
            //         //Animate the object or do something here
            //     }
            // }
        }
    }
}
