using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class TankEditor : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject point;
    public Canvas canvas;
    public GameObject myGameObject;
    int i = 0;

    const int PLAYER_LAYER = 7;
    private void Start()
    {
        point = GameObject.Find("PlayerEdit");
    }

    public void Save()
    {
        PlayerController.playerController.turrets.Clear();
        point.GetComponent<RectTransform>();
        ///snaps back player rotation to preven missaligment after rebuild
        PlayerController.player.transform.rotation = Quaternion.identity;

        foreach (Transform child in PlayerController.player.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in point.transform)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle((RectTransform)canvas.transform, child.transform.position, canvas.worldCamera, out Vector3 pos);
            if (child.gameObject.tag == "Hull")
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Hull\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), pos, child.transform.rotation) as GameObject;
            }
            else
            {
                myGameObject = Instantiate(Resources.Load(@"Prefabs\Weapons\Player\" + child.gameObject.name.Substring(0, child.gameObject.name.Length - 7), typeof(GameObject)), pos, child.transform.rotation) as GameObject;
                //SIDENOTE: get weapons by their tag please
                PlayerController.playerController.turrets.Add(myGameObject);
            }
            myGameObject.transform.localScale = child.transform.localScale;
            myGameObject.transform.SetParent(PlayerController.player.transform);
            myGameObject.GetComponent<SpriteRenderer>().sortingOrder = i;
            myGameObject.layer = PLAYER_LAYER;
            i++;
        }
        i = 0;
    }
}
