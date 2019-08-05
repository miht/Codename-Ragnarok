using UnityEngine;

public class RangedWeapon : Weapon
{

    public enum RangedWeaponTypes
    {
        Bow,
        Slingshot
    }

    protected RangedWeaponTypes _rangedWeaponType;

    [Range(0, 15f)]
    public float _range = 15f;

    public float Range
    {
        get { return _range; }
        set { _range = value; }
    }

    public RangedWeaponTypes GetRangedWeaponType()
    {
        return _rangedWeaponType;
    }
}
