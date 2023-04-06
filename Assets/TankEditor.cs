using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankEditor : MonoBehaviour
{
    public GameObject player;
    public GameObject point;
    private GameObject init;

    private void Start()
    {
        point = GameObject.Find("PlayerEdit");
        player = GameObject.Find("Player");
        /*foreach(Transform t in player.transform)
        {
            init = Instantiate(t.gameObject, point.transform) as GameObject;
            init.AddComponent<Dragger>();
        }*/
    }

    public void Save()
    {
        //destroy every part
        foreach (Transform child in player.transform)
        {
            Destroy(child.gameObject);
        }
        //rebuild here
        foreach (Transform child in point.transform)
        {
            Debug.Log(child.gameObject.name.ToString());
            GameObject myGameObject;
            //GameObject myGameObject = Instantiate(Resources.Load(@"Prefabs\"+child.gameObject.name.ToString(), typeof(GameObject)), child.transform.localPosition + player.transform.position, Quaternion.identity) as GameObject;
            Debug.Log(child.name.Length - 7);
            if(child.gameObject.tag == "Hull")
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Hull\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), child.transform.localPosition + player.transform.position, Quaternion.identity) as GameObject;
                myGameObject.transform.SetParent(player.transform);
            }
            else
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Weapons\Player\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), child.transform.localPosition + player.transform.position, Quaternion.identity) as GameObject;
                myGameObject.transform.SetParent(player.transform);
            }
            Camera.main.ScreenToViewportPoint(myGameObject.transform.position);
        }
    }
}
