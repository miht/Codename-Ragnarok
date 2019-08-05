﻿using UnityEngine;
using System.Collections.Generic;

public class Item : Entity
{
    public enum RaretyTypes {
        Prevalent,
        Rare,
        Epic,
        Legendary
    }

    public static Dictionary<RaretyTypes, Color> _raretyColors = new Dictionary<RaretyTypes, Color> {
        {RaretyTypes.Prevalent, new Color(1f, 1f, 1f)},
        {RaretyTypes.Rare, new Color(0f, 0f, 0.8f)},
        {RaretyTypes.Epic, new Color(0.6f, 0f, 1f)},
        {RaretyTypes.Legendary, new Color(1f, 0.7f, 0f)}
    };

    public RaretyTypes _rarety;
    public Sprite _uisprite;
    public Vector2Int _uiDimensions;

    public RaretyTypes Rarety {
        get { return _rarety;}
        set {_rarety = value;}
    }

    public Sprite UISprite
    {
        get { return _uisprite; }
        set { _uisprite = value; }
    }

    public Vector2Int Dimensions
    {
        get { return _uiDimensions; }
        set { _uiDimensions = value; }
    }
}
