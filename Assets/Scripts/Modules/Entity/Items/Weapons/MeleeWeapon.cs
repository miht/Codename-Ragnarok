using UnityEngine;

public class MeleeWeapon : Weapon {

    public enum MeleeWeaponTypes {
        Onehanded,
        Twohanded
    }

    protected MeleeWeaponTypes _meleeWeaponType;

    void Start() {
        base._weaponType = WeaponTypes.Melee;
    }

    [Range (0, 3f)]
    public float _range = 1.5f;
    [Range(0, 180)]
    public float _angle = 120f;
	
    public float Range {
        get {return _range; }
        set {_range = value; }
    }

     public float Angle {
        get {return _angle; }
        set {_angle = value; }
    }

    public MeleeWeaponTypes GetMeleeWeaponType() {
        return _meleeWeaponType;
    }
}
