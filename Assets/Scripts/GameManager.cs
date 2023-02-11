using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyList;
    public float gameDifficultyTreshold = 1f;
    public float gameDifficulty = 0f;
    private void Start()
    { 
        Application.targetFrameRate = 144;
    }
    
}
