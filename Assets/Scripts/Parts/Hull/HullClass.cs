using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullClass : MonoBehaviour
{
    [SerializeField]private string partName;
    [SerializeField]protected int health;
    private string x = "";
    private string y = "";

    /*public void getSize()
    {
        x = transform.localScale.x.ToString();
        y = transform.localScale.y.ToString();
    }

    public void setSize()
    {
        //transform.loclaScale.x = value from a slider for exaple.
    }*/

    /*public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public string hullName
    {
        get { return name; }
        set { name = value; }
    }*/

    /*public HullClass()
    {
        currentHealth = health;
        hullName = name;
    }*/
}
