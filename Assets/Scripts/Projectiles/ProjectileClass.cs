using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private Vector2 scale;

    protected void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Hull"))
            {
                Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());//collider for jumps between arenas
        }
    }

    protected void IgnoreEnemyCollision()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            foreach (Transform child in enemy[i].transform)
            {
                if (child.CompareTag("Hull"))
                {
                    Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
            }
        }
    }
    public virtual void Start()
    { 
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
