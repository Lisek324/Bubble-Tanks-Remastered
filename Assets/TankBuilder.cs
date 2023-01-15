using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBuilder : MonoBehaviour
{

    //public PlayerController playerController;
    public static bool isInEditMode = false;
    public GameObject tankBuilderUI;
    float temp;
    private void Start()
    {
        tankBuilderUI.SetActive(false);
        //temp = playerController.acceleration;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isInEditMode) EditMode();
            else Save();
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
        //playerController.acceleration = 0;
        isInEditMode = true;
        tankBuilderUI.SetActive(true);
    }

}
