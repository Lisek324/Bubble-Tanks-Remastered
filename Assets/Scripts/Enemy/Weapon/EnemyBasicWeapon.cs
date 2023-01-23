using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicWeapon : WeaponClass
{
    public Transform target;

    void Update()
    {
        RotateGun();
        Shoot();
    }
    private void RotateGun()
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }
    private void InstantiateProjectile()
    {
        Instantiate(getProjectilePrefab, getLaunchOffset.position, transform.rotation);
    }
    private void Shoot()
    {
        if (Time.time > getNextFire)
        {
            setNextFire = Time.time + fireRate;
            Invoke("InstantiateProjectile",0.05f);
        }   
    }
}
