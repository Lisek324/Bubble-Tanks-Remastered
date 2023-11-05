using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ArenaController : MonoBehaviour
{
    public Transform[] jumpPoints;
    private GameObject[] jumpPointsTemp;
    private Enemy enemy;
    private List<Transform> bubblesToCollect = new List<Transform>();

    Vector3 playerPos;

    private float elapsedTime;

    Vector3 tempJumpPoint;

    int index = 0;

    public bool enemiesSpawned = false;
    public bool isArenaCleared = false;
    private bool inJumpState = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collision.transform.SetParent(transform);
            ///jumpPoints cannot be active while player is inside that arena
            for (int i = 0; i < jumpPoints.Length; i++)
            {
                jumpPoints[i].gameObject.SetActive(false);
            }
            ///upon reentering arena, enemies are activated again
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy"))
                {
                    child.gameObject.SetActive(true);
                }
            }

            if (enemiesSpawned == false)
            {
                ///8 is maximum number of enemies in arena
                while (GameManager.gameManager.gameDifficulty < GameManager.gameManager.gameDifficultyTreshold && EnemyCounter(transform) < 8)
                {
                    int i = 0;
                    int random = Random.Range(0, GameManager.gameManager.enemyList.Count);
                    var enemyThreat = GameManager.gameManager.enemyList[random].GetComponent<Enemy>().threat;
                    index = random;
                    while (enemyThreat + GameManager.gameManager.gameDifficulty > GameManager.gameManager.gameDifficultyTreshold)
                    {
                        index = random - i;

                        enemyThreat = GameManager.gameManager.enemyList[index].GetComponent<Enemy>().threat;
                        i += 1;
                        if (index == 0)
                        {
                            break;
                        }
                    }
                    GameManager.gameManager.gameDifficulty += enemyThreat;
                    //TODO: add some random values to spawn enemies
                    var e = Instantiate(GameManager.gameManager.enemyList[index].gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    e.transform.parent = gameObject.transform;
                }
                GameManager.gameManager.gameDifficulty = 0;
                index = 0;
                enemiesSpawned = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && enemiesSpawned && collision.gameObject.activeSelf == true)
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            ///Just to be sure that enemies won't breathe in vacuum of space
            enemy.TakeDamage(int.MaxValue);
        }
        if (collision.CompareTag("Player"))
        {
            PlayerController.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerController.playerController.enabled = false;
            playerPos = collision.transform.position;
            
            inJumpState = true;
            tempJumpPoint = GetClosestJumpPoint().transform.position;
            ///reactivate jumpPoints after leaving arena
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
        if (inJumpState)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / 0.5f;
            PlayerController.player.transform.position = Vector3.Lerp(playerPos, tempJumpPoint, Mathf.SmoothStep(0,1,percentageComplete));
            if(PlayerController.player.transform.position == tempJumpPoint)
            {
                inJumpState = false;
                elapsedTime = 0;
                PlayerController.playerController.enabled = true;
            }
        }
        //this actualy makes my things go slower, and would be nice if gameDifficultyTreshold would just increase once after clearing an arena
        if (isArenaCleared == false && enemiesSpawned == true && EnemyCounter(transform) <= 0)
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
            GameManager.gameManager.gameDifficultyTreshold += 1;
        }

        if (isArenaCleared == true)
        {
            for (int i = 0; bubblesToCollect.Count > i; i++)
            {
                if (bubblesToCollect[i] != null)
                {
                    bubblesToCollect[i].transform.position = Vector3.MoveTowards(bubblesToCollect[i].transform.position, PlayerController.player.transform.position, 0.05f);
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
                currentDistance = Vector3.Distance(PlayerController.player.transform.position, go.transform.position);
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
                numberOfEnemies += 1;
            }
        }
        return numberOfEnemies;
    }


}
