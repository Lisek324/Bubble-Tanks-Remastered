using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicWeapon : WeaponClass
{
    void Update()
    {
        EnemyRotateGun();
        EnemyShooting();
    }
    protected override void EnemyRotateGun()
    {
        base.EnemyRotateGun();
    }


    protected override void EnemyShooting()
    {
        base.EnemyShooting();
    }
}
