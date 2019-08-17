using UnityEngine;

[System.Serializable]
public class Entity : ScriptableObject
{
    public string _name;
    public string _description;
    public int _id = -1;
    
    public string Name {
        get { return _name; }
        set {_name = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public int ID {
        get { return _id; }
        set {_id = value; }
    }
}
