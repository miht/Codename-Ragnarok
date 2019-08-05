using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : UIContainer
{
    private UIItem _item;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override UIItem ReplaceItem(UIItem item) {
        base.ReplaceItem(item);
        UIItem oldItem = _item;
        RemoveItem();
        AddItem(item);
        return oldItem;
    }

    public override void AddItem(UIItem item) {
        if (item == null)
            return;

        base.AddItem(item);

        item.SetDragged(false);
        item.SetValid(true);
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector2.zero;
        _item = item;
    }

    public override UIItem RemoveItem()
    {
        UIItem item = _item;
        if (item != null)
        {
            item.SetDragged(true);
            item.transform.SetParent(item.transform.parent.parent);
        }
        _item = null;
        return item;
    }

    public override UIItem GetItem()
    {
        return _item;
    }

    public override bool HasItem()
    {
        return _item != null;
    }
}
