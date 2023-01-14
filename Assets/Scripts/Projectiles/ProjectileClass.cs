using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected Rigidbody2D bullet;

    protected void ignorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
