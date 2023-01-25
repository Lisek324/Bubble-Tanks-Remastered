using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField]private int currentHealth;
    public GameObject[] bubbleDrop;
    Transform enemy;
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
        if(currentHealth == 0 || currentHealth < 0)
        {
            Destroy(gameObject);
            for (int i = 0; i < bubbleDrop.Length; i++)
            {
                Instantiate(bubbleDrop[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }
}
