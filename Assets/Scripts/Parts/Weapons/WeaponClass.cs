using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponClass : MonoBehaviour
{
    [Header("Weapon variables")]
    [SerializeField] protected float fireRate;
    protected float nextFire = 0f;
    [SerializeField] private Transform launchOffset;
    [SerializeField] private ProjectileClass projectilePrefab;
    [SerializeField] private AudioSource shootSound;
    protected virtual void EnemyRotateGun()
    {
        Vector2 direction = PlayerController.player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    protected void InstantiateProjectile()
    {
        Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        if (shootSound != null)
        {
            for (int i = 0; i < AudioSoundManager.sManager.audioSources.Count; i++)
            {

                if (AudioSoundManager.sManager.audioSources[i].clip == shootSound.clip && !shootSound.isPlaying)
                {
                    AudioSoundManager.sManager.audioSources[i].Play();
                }


            }
        }
    }
    protected virtual void EnemyShooting()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Invoke("InstantiateProjectile", 0.05f);
        }
    }

    protected virtual float PlayerShooting()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            InvokeRepeating("InstantiateProjectile", 0.05f, fireRate);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            CancelInvoke("InstantiateProjectile");
        }
        return nextFire;
    }
}
