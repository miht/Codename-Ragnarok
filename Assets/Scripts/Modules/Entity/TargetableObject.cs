using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObject : MonoBehaviour
{

    public enum TargetableObjectTypes {
        Obtainable,
        SomethingElseYo
    }
    protected TargetableObjectTypes _targetableObjectType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TargetableObjectTypes TargetableObjectType {
        get { return _targetableObjectType;}
        set {_targetableObjectType = value;}
    }
}
