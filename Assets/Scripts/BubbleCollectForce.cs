using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollectForce : MonoBehaviour
{
    private Rigidbody2D rib;
    public float dropForce = 5f;

    void Start()
    {
        rib = GetComponent<Rigidbody2D>();
        rib.AddForce(new Vector2((float)Random.Range(-dropForce, dropForce), (float)Random.Range(-dropForce, dropForce)), ForceMode2D.Impulse);
    }
}