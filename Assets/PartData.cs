using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PartData
{
    public string name;
    public float xPos;
    public float yPos;
    public float rWidth;
    public float rHeigth;
    public float xScale;
    public float yScale;
    public int cost;

    public PartData(string name, float xPos, float yPos, float rWidth, float rHeigth, float xScale, float yScale, int cost)
    {
        this.name = name;
        this.xPos = xPos;
        this.yPos = yPos;
        this.rWidth = rWidth;
        this.rHeigth = rHeigth;
        this.xScale = xScale;
        this.yScale = yScale;
        this.cost = cost;
    }
}
