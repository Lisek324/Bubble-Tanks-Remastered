using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : WeaponClass
{
    void Update()
    {
        PlayerShooting();
    }

    protected override float PlayerShooting()
    {
        return base.PlayerShooting();
    }

}
