using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCollectForce : MonoBehaviour
{
    private Rigidbody2D rig;
    public float dropForce = 10f;
    public int worth = 0;
    public static BubbleCollectForce bubbleCollectForce;

    void Start()
    {
        bubbleCollectForce = this;
        rig = GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector2((float)Random.Range(-dropForce, dropForce), (float)Random.Range(-dropForce, dropForce)), ForceMode2D.Impulse);
    }
}