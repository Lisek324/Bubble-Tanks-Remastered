using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponClass : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    protected float nextFire = 0f;

    [SerializeField] protected Transform launchOffset;
    [SerializeField] protected ProjectileClass projectilePrefab;
    [SerializeField] protected AudioSource shootSound;

    protected void instantiateProjectile()
    {
        Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        shootSound.Play();
    }

    protected void Shoot()
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
