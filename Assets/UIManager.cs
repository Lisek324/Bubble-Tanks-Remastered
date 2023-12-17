using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform mainCanvas;

    private void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public MessageBox CreateMessageBox()
    {
        GameObject msgGo =  Instantiate(Resources.Load("Prefabs/UI/MessageBox") as GameObject);
        return msgGo.GetComponent<MessageBox>();
    }
}
