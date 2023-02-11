using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField]private int currentHealth;
    public float threat = 0f;
    public GameObject[] bubbleDrop;
    Transform enemy;
    public bool isDead = false;

    void Start()
    {
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
        if(currentHealth <= 0)
        {
            isDead = true;
            Destroy(gameObject);
            for (int i = 0; i < bubbleDrop.Length; i++)
            {
                Instantiate(bubbleDrop[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }
}
