using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSpawner : MonoBehaviour
{
    ArenaTemplate arenaTemplate;
    public int direction; //1-left, 2-up, 3-right, 4-down
    void Start()
    {
        arenaTemplate = GameObject.FindGameObjectWithTag("Arena").GetComponent<ArenaTemplate>();
        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        switch (direction)
        {
            case 1:
                Instantiate(arenaTemplate.ArenaLeft[0], transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(arenaTemplate.ArenaLUR[0], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(arenaTemplate.ArenaRight[0], transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(arenaTemplate.ArenaLDR[0], transform.position, Quaternion.identity);
                break;
        }
        Destroy(gameObject);
    }
}

