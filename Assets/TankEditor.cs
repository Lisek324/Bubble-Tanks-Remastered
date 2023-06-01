using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankEditor : MonoBehaviour
{
    public GameObject player;
    public GameObject point;
    public Canvas canvas;
    public PlayerController playerController;
    public GameObject myGameObject;

    private void Start()
    {
        point = GameObject.Find("PlayerEdit");
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public void Save()
    {
        ///snaps back player rotation to x axis to preven missaligment after rebuild
        player.transform.rotation = Quaternion.identity;

        ///destroy every part
        foreach (Transform child in player.transform)
        {
            Destroy(child.gameObject);
        }
        ///rebuild here
        foreach (Transform child in point.transform)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)canvas.transform, child.transform.position, canvas.worldCamera, out Vector3 pos);
            if (child.gameObject.tag == "Hull")
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Hull\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), pos, child.transform.rotation) as GameObject;
            }
            else
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Weapons\Player\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), pos, Quaternion.identity) as GameObject;
            }
            myGameObject.transform.localScale = child.transform.localScale;
            myGameObject.transform.SetParent(player.transform);
        }        
    }
}
