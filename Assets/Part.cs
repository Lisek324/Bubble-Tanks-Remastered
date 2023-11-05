using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    public int partCost;
    public static Part part;
    private void Awake()
    {
        part = this;
    }
}
