using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
    public static GameObject dragger;
    GameObject partInventory;
    Transform tb;
    Transform builder;
    private Slider rotationSlider;
    private Slider scaleSlider;

    public void Start()
    {
        rotationSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();

        builder = GameObject.Find("PlayerEdit").GetComponent<Transform>();

        tb = GameObject.Find("HUD").GetComponent<Transform>();
        partInventory = GameObject.Find("ConstructionZone");

        scaleSlider.onValueChanged.AddListener(delegate
        {
            Scale();
        });

        rotationSlider.onValueChanged.AddListener(delegate {
            Rotate();
        });

       
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.parent.name == "BasePage")
        {
            dragger = Instantiate(gameObject, transform);
        }
        else
        {
            dragger = gameObject;
            rotationSlider.value = dragger.transform.eulerAngles.z / 360;
            //scaleSlider.value = dragger.transform.localScale.magnitude;
        }
        dragger.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragger.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)partInventory.transform, dragger.transform.position))
        {
            dragger.transform.SetParent(builder);
        }
        else
        {
            Destroy(dragger.gameObject);
        }
    }
    public void OnInitializePotentialDrag(PointerEventData eventData)
    {

        eventData.useDragThreshold = false;
    }

    public void Rotate()
    {
        dragger.transform.eulerAngles = Vector3.forward* rotationSlider.value*360;
    }

    public void Scale()
    {
        dragger.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
    }
}
