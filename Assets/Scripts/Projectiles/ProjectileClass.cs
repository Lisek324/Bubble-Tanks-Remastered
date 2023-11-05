using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private Vector2 scale;
    
    public virtual void Start()
    {
        //TODO:UnassignedReferenceException: The variable bullet of Projectile has not been assigned.
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    protected virtual void DealDamage(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")==true)
        {
            collision.gameObject.TryGetComponent(out Enemy enemyComponent);
            if (enemyComponent.isDead == false)
            {
                enemyComponent.TakeDamage(damage);
            }
        }
        else if(collision.gameObject.CompareTag("Player") == true)
        {
            collision.gameObject.TryGetComponent(out PlayerController playerComponent);
            playerComponent.TakeDamage(damage);
        }
        damage = 0;
        Destroy(gameObject);
    }

    protected void DestroyProjectile(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            //TO DO: you can still shoot thgrough arena tho
            Destroy(gameObject);
        }
    }
}
