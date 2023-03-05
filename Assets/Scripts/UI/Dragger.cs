using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IInitializePotentialDragHandler
{
    GameObject dragger;
    Transform builder;
    public void Start()
    {
        builder = GameObject.Find("PlayerEdit").GetComponent<Transform>();
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
    }

    public void OnDrag(PointerEventData eventData)
    {

        //rectTransform.anchoredPosition += eventData.delta /canvas.scaleFactor;
        dragger.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragger.transform.SetParent(builder);
        dragger.transform.SetAsLastSibling();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //eventData.useDragThreshold = false;
    }
}
