using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public int worldSize=1;
    private GameObject borderLeft;
    private GameObject borderUp;
    private GameObject borderRight;
    private GameObject borderDown;

    private void Start()
    {
        borderLeft = GameObject.Find("BorderLeft");
        borderUp = GameObject.Find("BorderUp");
        borderRight = GameObject.Find("BorderRight");
        borderDown = GameObject.Find("BorderDown");

        borderLeft.transform.position = new Vector2(worldSize * -125,0);
        borderUp.transform.position = new Vector2(0, worldSize * 125);
        borderRight.transform.position = new Vector2(worldSize * 125,0);
        borderDown.transform.position = new Vector2(0, worldSize * -125);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
