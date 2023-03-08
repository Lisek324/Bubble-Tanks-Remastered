using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField] private int currentHealth;
    public int threat = 0;
    public GameObject[] bubbleDrop;
    //Transform enemy;
    GameObject player;
    GameObject destroyedEnemy;
    public bool isDead = false;
    //ArenaController a;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ///for every enemy hull collider, ignore with player trigger collider
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Hull"))
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), child.GetComponent<Collider2D>());
            }
        }

        //a = gameObject.AddComponent<ArenaController>();
        currentHealth = GetHealth(transform);
        ///ignore player trigger collider with enemy trigger collider
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
    }

    protected override int GetHealth(Transform entity)
    {
        return base.GetHealth(entity);
    }

    public void TakeDamage(int damageAmmount)
    {

        currentHealth = currentHealth - damageAmmount;
        if (currentHealth <= 0)
        {
            isDead = true;
            
            Destroy(gameObject);
            for (int i = 0; i < bubbleDrop.Length; i++)
            {
                //Instantiate(bubbleDrop[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                var bubble = Instantiate(bubbleDrop[i], transform.position, Quaternion.identity);
                bubble.transform.parent = transform.parent;

            }
            /*a.EnemyCounter(transform.parent);
            Debug.Log(a.EnemyCounter(transform.parent));*/
        }
       
    }
}
