using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemyProjectile : ProjectileClass
{
    public override void Start()
    {
        base.Start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {    
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.TakeDamage(damage);
        }
        damage = 0;
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
