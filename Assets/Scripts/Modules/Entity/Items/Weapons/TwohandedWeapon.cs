using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwohandedWeapon : MeleeWeapon {

    public enum TwohandedWeaponTypes {
        Axe,
        Sword,
        Spear
    }

    protected TwohandedWeaponTypes _twohandedWeaponType;

    public TwohandedWeapon() {
        _meleeWeaponType = MeleeWeaponTypes.Twohanded;
    }

    public TwohandedWeaponTypes GetTwohandedWeaponType() {
        return _twohandedWeaponType;
    }
}
