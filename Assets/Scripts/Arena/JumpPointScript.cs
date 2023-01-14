using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPointScript : MonoBehaviour
{
    //ArenaController arenaController;
    private void Start()
    {
        //arenaController = GameObject.FindGameObjectWithTag("Arena").GetComponent<ArenaController>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO: Try making JumpParent with array and assign JumpPoints to it
        //every jumppoint has an array with other jumppoints and it makes no sense
        if (collision.CompareTag("Player"))
        {

        }
    }
}

