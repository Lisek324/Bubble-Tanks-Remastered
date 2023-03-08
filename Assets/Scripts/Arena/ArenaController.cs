using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArenaController : MonoBehaviour
{
    public GameObject player;
    public Transform[] jumpPoints;
    private GameObject[] jumpPointsTemp;
    private GameObject[] enemies;
    private Enemy enemy;
    private List<Transform> bubblesToCollect = new List<Transform>();
    //private GameObject[] bubblesToCollect;
    //public Transform currentArena;

    private GameManager gameManager;
    int index = 0;

    public bool isSpawned = false;
    public bool isArenaCleared = false;

    private void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //currentArena = transform;
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            ///jumpPoints cannot be active while player is inside that arema
            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(false);
            }
            ///upon reentering arena, enemies are activated again
            foreach(Transform child in transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    child.gameObject.SetActive(true);
                }
            }
           
            if (isSpawned == false)
            {
                ///8 is maximum number of enemies in arena
                while (gameManager.gameDifficulty < gameManager.gameDifficultyTreshold && EnemyCounter(transform) < 8)
                {
                    int i = 0;
                    int random = Random.Range(0, gameManager.enemyList.Count);
                    var enemyThreat = gameManager.enemyList[random].GetComponent<Enemy>().threat;
                    index = random;
                    while(enemyThreat + gameManager.gameDifficulty > gameManager.gameDifficultyTreshold)
                    { 
                        index = random - i;
                        
                        enemyThreat = gameManager.enemyList[index].GetComponent<Enemy>().threat;
                        i += 1;
                        if (index == 0)
                        {
                            break;
                        }
                    }
                    
                    gameManager.gameDifficulty += enemyThreat;
                    var e = Instantiate(gameManager.enemyList[index].gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    e.transform.parent = gameObject.transform;          
                }
                gameManager.gameDifficulty = 0;
                index = 0;
                isSpawned = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isSpawned && collision.gameObject.activeSelf == true)
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(int.MaxValue);
            Debug.Log("DESTROYED");
        }
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(GetClosestJumpPoint().transform.position.x, GetClosestJumpPoint().transform.position.y);

            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(true);
            }

            ///upon leaving arena, enemies will be deactivated
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        
    }


    private void Update()
    {

        if (isArenaCleared == false && isSpawned == true && EnemyCounter(transform) <= 0)
        {
            isArenaCleared = true;
            foreach (Transform t in transform)
            {
                if (t.CompareTag("CollectBubble"))
                {
                    bubblesToCollect.Add(t);
                }
            }
            //TODO: better formula for game difficulty is needed
            gameManager.gameDifficultyTreshold += 1;
        }
        
        if (isArenaCleared == true)
        {
            Debug.Log("im here");
            for (int i = 0; bubblesToCollect.Count > i; i++)
            {
                if (bubblesToCollect[i] != null)
                {
                    bubblesToCollect[i].transform.position = Vector3.MoveTowards(bubblesToCollect[i].transform.position, player.transform.position, 0.05f);
                }
                else
                {
                    bubblesToCollect.RemoveAt(i);
                }
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
    public int EnemyCounter(Transform tr)
    {
        int numberOfEnemies = 0;
        foreach (Transform t in tr)
        {     
            if (t.CompareTag("Enemy"))
            {   
                numberOfEnemies +=1;
            }
        }
        //TODO: There must be better way to check after a frame the ammount of enemies in arena
        //possible solution: get current position(arena) of a player then after frame check the number
        return numberOfEnemies;
    }
}
