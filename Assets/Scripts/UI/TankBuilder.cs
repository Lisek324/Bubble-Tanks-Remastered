using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankBuilder : MonoBehaviour
{
    public static bool isInEditMode = false;
    public GameObject tankBuilderUI;
    float temp;
    private void Start()
    {
        tankBuilderUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isInEditMode) EditMode();
            else 
            {
                Save();
                Time.timeScale = 1.0f;
                PlayerController.playerController.SetHealth();
            }
                
        }
    }
    public void Save()
    {
        tankBuilderUI.SetActive(false);

        //playerController.acceleration = temp;
        isInEditMode = false;
    }
    public void EditMode()
    {
        Time.timeScale = 0.0f;
        //playerController.acceleration = 0;
        isInEditMode = true;
        tankBuilderUI.SetActive(true);
    }

}
