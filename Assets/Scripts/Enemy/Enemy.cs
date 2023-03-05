using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField] private int currentHealth;
    public int threat = 0;
    public GameObject[] bubbleDrop;
    Transform enemy;
    GameObject destroyedEnemy;
    public bool isDead = false;
    ArenaController a;

    void Start()
    {
        a = gameObject.AddComponent<ArenaController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        currentHealth = GetHealth(enemy);
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
