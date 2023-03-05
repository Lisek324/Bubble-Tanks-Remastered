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
        /*foreach(Transform t in player.transform)
        {
            init = Instantiate(t.gameObject, point.transform) as GameObject;
            init.AddComponent<Dragger>();
        }*/
    }

    public void Save()
    {
        foreach (Transform child in player.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in point.transform)
        {
            Instantiate(child.gameObject, player.transform.position+child.transform.position, Quaternion.identity).transform.SetParent(player.transform);
        }
    }
}
