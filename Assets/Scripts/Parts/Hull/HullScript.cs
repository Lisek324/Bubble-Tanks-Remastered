using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HullScript : MonoBehaviour
{
    [SerializeField] public string partName;
    [SerializeField] public int partHealth;
    [SerializeField] private int cost;
    public Sprite sprite;

    private void Start()
    {
        sprite = GetComponent<Sprite>();
    }
}
