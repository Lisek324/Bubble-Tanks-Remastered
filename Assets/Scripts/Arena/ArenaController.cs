using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArenaController : MonoBehaviour
{
    public GameObject player;
    public bool isInJumpingState = false;
    public float speed = 1f;
    public Transform[] jumpPoints;
    private GameObject[] jumpPointsTemp;
    public GameObject[] numberOfEnemies;
    public GameObject[] numberOfEnemiesTemp;
    private void Start()
    {
        numberOfEnemiesTemp = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = new Vector3(GetClosestJumpPoint().transform.position.x, GetClosestJumpPoint().transform.position.y);
            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(true);
            }
        }
    }

    GameObject GetClosestJumpPoint()
    {
        jumpPointsTemp = GameObject.FindGameObjectsWithTag("JumpPoint");
        float closestDistance = Mathf.Infinity;
        GameObject tr = null;
        foreach (GameObject go in jumpPointsTemp)
        {
            if (go.gameObject.activeSelf)
            {
                float currentDistance;
                currentDistance = Vector3.Distance(player.transform.position, go.transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    tr = go.gameObject;
                }
            }
        }
        return tr;
    }
}
