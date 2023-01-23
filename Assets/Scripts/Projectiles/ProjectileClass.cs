using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D bullet;

    protected void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    protected void IgnoreEnemyCollision()
    {
        GameObject enemy= GameObject.FindGameObjectWithTag("Enemy");
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    protected void BulletSpeed()
    {
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    public int getDamage
    {
        get { return damage; }
    }
}
