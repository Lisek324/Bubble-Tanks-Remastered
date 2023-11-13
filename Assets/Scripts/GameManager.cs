using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyList;
    [SerializeField] private List<AudioSource> audioSource;
    public int gameDifficultyTreshold = 1;
    public int gameDifficulty = 0;
    public static GameManager gameManager;
    public int bubbles = 0;
    private void Awake()
    {
        gameManager = this;
        Application.targetFrameRate = 144;
    }
}
