using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeaponProjectile : ProjectileClass
{ 

    private void Start()
    {
        ignorePlayerCollision();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            //TO DO: you can still shoot thgrough arena tho
            Destroy(gameObject);
        }
    }
}
