using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIEquipmentSlot : UISlot
{
    public Image _backgroundImage;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AddItem(UIItem item) {
        _backgroundImage.enabled = item == null;
        base.AddItem(item);
    }

    public override UIItem RemoveItem() {
        return base.RemoveItem();
    }
}
