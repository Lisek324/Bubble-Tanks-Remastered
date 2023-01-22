using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponClass : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    protected static float nextFire = 0f;

    [SerializeField] protected Transform launchOffset;
    [SerializeField] protected ProjectileClass projectilePrefab;
    [SerializeField] protected AudioSource shootSound;

}
