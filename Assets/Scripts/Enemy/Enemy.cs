using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField] private int health, maxHealth = 3;
    public GameObject[] bubbleDrop;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageAmmount) 
    { 
        health = health - damageAmmount;
        if(health <= 0)
        {
            Destroy(gameObject);
            for (int i= 0;i<bubbleDrop.Length; i++)
            {
                Instantiate(bubbleDrop[i], transform.position + new Vector3(0,1,0), Quaternion.identity);
            }
        }
    }
}
