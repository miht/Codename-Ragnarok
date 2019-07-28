using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Entities/Items/Weapons/Sword", order = 1)]
public class Sword : OnehandedWeapon {
    public Sword() {
        base._onehandedWeaponType = OnehandedWeaponTypes.Sword;
    }
}

