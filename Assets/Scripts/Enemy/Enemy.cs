using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityClass
{
    [SerializeField] private int currentHealth;
    public int threat = 0;
    public GameObject[] bubbleDrop;
    public bool isDead = false;

    void Start()
    {
        currentHealth = GetHealth(transform);
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
                var bubble = Instantiate(bubbleDrop[i], transform.position, Quaternion.identity);
                bubble.transform.parent = transform.parent;
            }
        }

    }
}
