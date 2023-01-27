using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankEditor : MonoBehaviour
{
    public GameObject player;
    public GameObject point;
    public GameObject grid;

    public Dragger dragger;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void BuyPart(HullScript hc)
    {
        
    }
}
