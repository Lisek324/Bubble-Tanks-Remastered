using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityClass : MonoBehaviour
{
    private int health;

    /*private void TakeDamage(int damageAmmount)
    {
        health = health - damageAmmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            for (int i = 0; i < bubbleDrop.Length; i++)
            {
                Instantiate(bubbleDrop[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }*/
}
