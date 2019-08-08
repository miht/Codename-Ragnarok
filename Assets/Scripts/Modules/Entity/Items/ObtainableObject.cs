using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableObject : TargetableObject
{
    private BoxCollider _collider;

    [Range(0, 1)]
    public float _activationTime;

    public Item _item;
    // Start is called before the first frame update
    void Start()
    {
        _targetableObjectType = TargetableObjectTypes.Obtainable;
        _collider = GetComponent<BoxCollider>();

        Invoke("Activate", _activationTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetItem() {
        return _item;
    }

    public void Activate() {
        _collider.enabled = true;
    }
}
