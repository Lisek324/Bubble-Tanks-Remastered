using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallProjectile : ProjectileClass
{
    public override void Start()
    {
        base.Start();
        IgnorePlayerCollision();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemyComponent))
        {
            if(enemyComponent.isDead == false)
            {
                enemyComponent.TakeDamage(damage);
            }
        }
        damage = 0;
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
