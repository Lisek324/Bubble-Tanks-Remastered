using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponClass : MonoBehaviour
{
    [SerializeField] protected float fireRate;
    private float nextFire = 0f;

    [SerializeField] private Transform launchOffset;
    [SerializeField] private ProjectileClass projectilePrefab;
    [SerializeField] private AudioSource shootSound;

    [SerializeField]
    public ProjectileClass getProjectilePrefab
    {
        get { return projectilePrefab; }
    }

    public Transform getLaunchOffset
    {
        get { return launchOffset; }
    }

    public AudioSource getShootSound
    {
        get { return shootSound; }
    }

    public float getNextFire
    {
        get { return nextFire; }
    }

    public float setNextFire
    {
        set {  nextFire = value; }
    }

}
