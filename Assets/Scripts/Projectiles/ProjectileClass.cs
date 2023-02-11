using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileClass : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;
    [SerializeField] private Rigidbody2D bullet;

    protected void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Hull"))
            {
                Physics2D.IgnoreCollision(child.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
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
