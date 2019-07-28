using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainableObject : TargetableObject
{
    public Item _item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetItem() {
        return _item;
    }
}
