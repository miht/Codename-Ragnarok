using UnityEngine;

[System.Serializable]
public class Entity : ScriptableObject
{
    public string _name;
    public int _id = -1;
    public GameObject _uiItem;
    
    public string Name {
        get { return _name; }
        set {_name = value; }
    }

    public int ID {
        get { return _id; }
        set {_id = value; }
    }

    public GameObject UIItem {
        get { return _uiItem; }
        set { _uiItem = value; }
    }
}
