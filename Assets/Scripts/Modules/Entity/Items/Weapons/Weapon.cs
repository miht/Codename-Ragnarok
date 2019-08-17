using UnityEngine;

public class Weapon : Equippable
{
    public enum WeaponTypes {
        Melee,
        Ranged
    }

    protected WeaponTypes _weaponType;
    public int _damageValue = 0;

    public Weapon() {
        base._equippableType = EquippableTypes.Weapon;
    }

    public WeaponTypes WeaponType {
        get { return _weaponType; }
        set { _weaponType = value; }
    }

    public int DamageValue {
        get { return _damageValue; }
        set { _damageValue = value; }
    }
}
