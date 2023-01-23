using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HullScript : HullClass
{  
    public int hullHealth
    {
        get { return health; }
        set { health = value; }
    }

    public string hullName
    {
        get { return name; }
        set { name = value; }
    }
}
