using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArenaController : MonoBehaviour
{
    public GameObject player;
    public Transform[] jumpPoints;
    private GameObject[] jumpPointsTemp;
    public int numberOfEnemies = 0;
    private GameManager gameManager;
    
    public bool isSpawned = false;
    public bool isArenaCleared = false;
   

    private void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //player.transform.SetParent(transform);
            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(false);
            }
            //
            if (isSpawned == false)
            {
                //8 is maximum number of enemies in arena
                while (gameManager.gameDifficulty < gameManager.gameDifficultyTreshold)
                {
                    Debug.Log(gameManager.enemyList.Count);
                    int random = Random.Range(0, gameManager.enemyList.Count);
                    Instantiate(gameManager.enemyList[random].gameObject, transform.position,Quaternion.identity);
                    gameManager.gameDifficulty += gameManager.enemyList[random].GetComponent<Enemy>().threat;
                    Debug.Log(gameManager.gameDifficultyTreshold);
                    numberOfEnemies++;
                }
                gameManager.gameDifficulty = 0;
                isSpawned = true;
            }
            if (isArenaCleared == false && numberOfEnemies == 0)
            {
                isArenaCleared = true;
                //TODO: better formula for game difficulty is needed
                gameManager.gameDifficultyTreshold += 0.5f;
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
            if (go.activeSelf)
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
