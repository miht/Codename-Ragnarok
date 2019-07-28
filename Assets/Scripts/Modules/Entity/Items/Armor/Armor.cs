using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Entities/Items/Armor", order = 1)]
public class Armor : Equippable
{
    public enum ArmorTypes {
        Head,
        Chest,
        Hands,
        Waist,
        Feet
    }

    public ArmorTypes _armorType;
    public int _armorValue = 0;

    public ArmorTypes ArmorType {
        get { return _armorType; }
        set { _armorType = value; }
    }

    public int ArmorValue {
        get { return _armorValue; }
        set { _armorValue = value; }
    }
}
