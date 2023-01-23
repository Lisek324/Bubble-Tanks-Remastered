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
        Instantiate(getProjectilePrefab, getLaunchOffset.position, transform.rotation);
        getShootSound.Play();
    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > getNextFire)
        {
            setNextFire = Time.time + fireRate;
            InvokeRepeating("instantiateProjectile", 0.05f, fireRate);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("instantiateProjectile");
        }
    }


}
