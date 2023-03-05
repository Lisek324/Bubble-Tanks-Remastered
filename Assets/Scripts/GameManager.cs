using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyList;
    public int gameDifficultyTreshold = 1;
    public int gameDifficulty = 0;
    private void Start()
    { 
        Application.targetFrameRate = 144;
    }
    
}
