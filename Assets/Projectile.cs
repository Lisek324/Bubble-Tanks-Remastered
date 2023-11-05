using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : ProjectileClass
{
    public override void Start()
    {
        base.Start();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DealDamage(collision);
    }

    private void OnTriggerExit2D(Collider2D arenaCollision)
    {
        DestroyProjectile(arenaCollision);
    }

    protected override void DealDamage(Collision2D collision)
    {
        base.DealDamage(collision);
    }
}
