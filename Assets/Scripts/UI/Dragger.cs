using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IInitializePotentialDragHandler
{
    GameObject dragger;
    Transform tb;
    Transform builder;
    string originalName = "";
    public GameObject player;
    public void Start()
    {
        builder = GameObject.Find("PlayerEdit").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        tb = GameObject.Find("HUD").GetComponent<Transform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.name == "Background")
        {
            dragger = Instantiate(gameObject, transform);
        }
        else
        {
            dragger = gameObject;
        }
        originalName = dragger.gameObject.name;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragger.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        dragger.transform.SetAsLastSibling();
        dragger.transform.SetParent(builder);
        
        /*var a = Instantiate(gameObject, new Vector3(player.transform.position.x +dragger.transform.position.x, player.transform.position.y+ dragger.transform.position.y), Quaternion.identity);
        a.transform.SetParent(player.transform);
        var x = new Vector2(player.transform.position.x, player.transform.position.y);
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(a.GetComponent<RectTransform>(), tb.GetComponent<RectTransform>().transform.position, null, out x);
        Camera.main.ScreenToViewportPoint(a.transform.position);*/

        //var a = Instantiate(gameObject, new Vector3((tb.transform.position.x / dragger.transform.position.x), (tb.transform.position.y / dragger.transform.position.y)), Quaternion.identity);
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(a.GetComponent<RectTransform>(), tb.GetComponent<RectTransform>().transform.position+dragger.transform.position, null, out x);

        //Debug.Log(Camera.main.ScreenToViewportPoint(a.transform.position).ToString());

        /*var a = Instantiate(gameObject, dragger.transform.localPosition+player.transform.position, Quaternion.identity);

        a.transform.SetParent(player.transform);
        var x = new Vector2(player.transform.position.x, player.transform.position.y);
        
        Camera.main.ScreenToViewportPoint(a.transform.position);*/
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        /*foreach (Transform child in player.transform)
         {
             Destroy(child.gameObject);
         }*/

        Debug.Log(originalName);

    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //eventData.useDragThreshold = false;
    }
}
