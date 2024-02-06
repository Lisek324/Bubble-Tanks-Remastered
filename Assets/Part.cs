using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{

    public static Part part;
    public int partCost;
    public float partMass;
    private void Awake()
    {
        part = this;
    }
}
