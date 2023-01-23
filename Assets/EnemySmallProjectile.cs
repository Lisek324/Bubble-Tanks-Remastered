using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallProjectile : ProjectileClass
{

    private void Start()
    {
        IgnoreEnemyCollision();
        BulletSpeed();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController playerComponent))
        {
            //playerComponent.
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
