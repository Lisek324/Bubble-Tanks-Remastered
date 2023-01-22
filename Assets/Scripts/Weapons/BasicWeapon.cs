using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : WeaponClass
{
    void Update()
    {
        Shoot();
    }
    private void instantiateProjectile()
    {
        Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        shootSound.Play();
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            InvokeRepeating("instantiateProjectile", 0.05f, fireRate);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("instantiateProjectile");
        }
    }
}
