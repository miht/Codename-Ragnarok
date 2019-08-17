using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spear", menuName = "Entities/Items/Weapons/Spear", order = 1)]
public class Spear : TwohandedWeapon
{
    public Spear()
    {
        base._twohandedWeaponType = TwohandedWeaponTypes.Spear;
    }
}

