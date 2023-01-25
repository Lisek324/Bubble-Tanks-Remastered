using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallProjectile : ProjectileClass
{

    private void Start()
    {
        BulletSpeed();
        IgnoreEnemyCollision();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.TakeDamage(getDamage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            //TO DO: it can still shoot thgrough arena
            Destroy(gameObject);
        }
    }
}
