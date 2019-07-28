using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnehandedWeapon : MeleeWeapon {

    public enum OnehandedWeaponTypes {
        Axe,
        Sword,
        Dagger
    }

    protected OnehandedWeaponTypes _onehandedWeaponType;

    public OnehandedWeapon() {
        _meleeWeaponType = MeleeWeaponTypes.Onehanded;
    }

    public OnehandedWeaponTypes GetOnehandedWeaponType() {
        return _onehandedWeaponType;
    }
}
