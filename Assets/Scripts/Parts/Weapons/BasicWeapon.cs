using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : WeaponClass
{
    private float nextFire = 0f;
    void Update()
    {
        nextFire = PlayerShooting(nextFire);
    }

    protected override float PlayerShooting(float nextFire)
    {
        return base.PlayerShooting(nextFire);
    }
}
