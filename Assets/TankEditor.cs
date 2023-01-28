using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankEditor : MonoBehaviour
{
    public GameObject player;
    public GameObject point;
    private GameObject init;

    private void Start()
    {
        point = GameObject.Find("PlayerEdit");
        player = GameObject.Find("Player");
        foreach(Transform t in player.transform)
        {
            init = Instantiate(t.gameObject, point.transform) as GameObject;
            init.AddComponent<Dragger>();
        }
    }
}
