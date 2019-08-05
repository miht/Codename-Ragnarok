using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Entities/Items/Weapons/Bow", order = 1)]
public class Bow : RangedWeapon {
    public Bow() {
        base._rangedWeaponType = RangedWeaponTypes.Bow;
    }
}

