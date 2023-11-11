using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
/// <summary>
/// Spaghetti is my favourite dish
/// </summary>

public class Dragger : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IPointerDownHandler, ISelectHandler, IDeselectHandler, IPointerUpHandler, IPointerClickHandler
{
    public static HashSet<Dragger> allSelectable = new HashSet<Dragger>();
    public static HashSet<Dragger> currentlySelected = new HashSet<Dragger>();
    public static List<GameObject> list = new List<GameObject>();
    bool dragmultiple = false;
    Image myImage;
    [SerializeField]
    Color unselectedColor;
    [SerializeField]
    Color selectedColor;

    public bool snapToGrid = true;
    public bool xMirror = true;

    [SerializeField] float gridSize = 2f;
    float min_x, max_x, min_y, max_y;

    [SerializeField] public static Canvas canvas;
    [SerializeField] private TextMeshProUGUI bubbles;

    public static GameObject cursor;
    public GameObject selectionBox;
    //public static GameObject cursor2;

    GameObject partInventory;
    static RectTransform container;
    static RectTransform builder;
    private Slider rotationSlider;
    private Slider scaleSlider;

    const int UI_HEIGHT = 1080;

    public void Start()
    {

        canvas = GameObject.Find("HUD").GetComponent<Canvas>();
        bubbles = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();

        rotationSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();

        container = GameObject.Find("Container").GetComponent<RectTransform>();
        builder = GameObject.Find("PlayerEdit").GetComponent<RectTransform>();

        partInventory = GameObject.Find("ConstructionZone");

        if (cursor != null)
        {
            scaleSlider.onValueChanged.AddListener(delegate
            {
                Scale();
            });

            rotationSlider.onValueChanged.AddListener(delegate
            {
                Rotate();
            });
        }
        myImage = GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {

        if (currentlySelected.Count >= 2)
        {
            foreach (Transform child in builder)
            {
                foreach(Transform child2 in child)
                {
                    
                    if (child2.gameObject.activeSelf.Equals(true))
                    {
                        child.transform.SetParent(container);
                    }
                }
                
            }
            dragmultiple = true;
        }
        /*else
         {
             foreach (Transform child in container)
             {
                 child.transform.SetParent(builder);
             }
         }*/
        if (dragmultiple)
        {
            container.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        else
        {
            min_x = max_x = transform.localPosition.x;
            min_y = max_y = transform.localPosition.y;

            if (snapToGrid)
            {
                cursor.transform.position = new Vector2(Mathf.RoundToInt(eventData.position.x / gridSize) * gridSize, Mathf.RoundToInt(eventData.position.y / gridSize) * gridSize);
                //cursor2.transform.position = new Vector2(cursor.transform.position.x, -cursor.transform.position.y + UI_HEIGHT - 220);//Im going to throw up
            }
            if (cursor.transform.localPosition.y <= 10 && cursor.transform.localPosition.y >= -10)
            {
                cursor.transform.localPosition = new Vector2(cursor.transform.localPosition.x, 0);
            }
            if (cursor.transform.localPosition.x <= 10 && cursor.transform.localPosition.x >= -10)
            {
                cursor.transform.localPosition = new Vector2(0, cursor.transform.localPosition.y);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        while (container.childCount > 0)
        {
            container.GetChild(0).transform.SetParent(builder);
        }
        //if (cursor.transform.position == cursor2.transform.position)
        //{
        //Destroy(cursor2.gameObject);
        //}
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)partInventory.transform, cursor.transform.position))
        {
            //cursor.transform.SetParent(builder);
            Destroy(cursor.GetComponent<TooltipTrigger>());
            //cursor2.transform.SetParent(builder);

            foreach (RectTransform child in transform.parent)
            {
                Vector2 scale = child.sizeDelta;
                float temp_min_x, temp_max_x, temp_min_y, temp_max_y;

                temp_min_x = child.localPosition.x - (scale.x / 2);
                temp_max_x = child.localPosition.x + (scale.x / 2);
                temp_min_y = child.localPosition.y - (scale.y / 2);
                temp_max_y = child.localPosition.y + (scale.y / 2);

                if (temp_min_x < min_x)
                    min_x = temp_min_x;
                if (temp_max_x > max_x)
                    max_x = temp_max_x;

                if (temp_min_y < min_y)
                    min_y = temp_min_y;
                if (temp_max_y > max_y)
                    max_y = temp_max_y;

                //GameManager.gameManager.bubbles -= Part.part.partCost;
            }
            builder.GetComponent<RectTransform>().sizeDelta = new Vector2(max_x - min_x, max_y - min_y);
        }
        list.Clear();
        foreach (Transform selectables in builder)
        {
            allSelectable.Add(selectables.GetComponent<Dragger>());
        }
        bubbles.text = "Bubbels: " + GameManager.gameManager.bubbles.ToString();

        //currentlySelected.Add(cursor.GetComponent<Dragger>());
    
        //redundant?
        cursor.transform.GetChild(0).gameObject.SetActive(true);
        //cursor.GetComponent<Image>().color = selectedColor;
        dragmultiple = false;
        cursor.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
        
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dragmultiple == false)
        {
            if (gameObject.transform.parent.name == "Background")
            {
                cursor = Instantiate(gameObject, transform);
                cursor.transform.SetParent(builder);
                
                if (xMirror)
                {
                    //cursor2 = Instantiate(gameObject, transform);
                    //cursor2.transform.SetParent(builder);
                }
            }
            else
            {
                cursor = gameObject;
                rotationSlider.value = cursor.transform.eulerAngles.z / 360;
            }
        }
        OnSelect(eventData);

        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        else
        {
            cursor.transform.SetParent(container);
        }
        cursor.transform.SetAsLastSibling();
        // cursor2.transform.SetAsLastSibling();
        //cursor.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)partInventory.transform, cursor.transform.position))
        {
            Debug.Log("object ahs benn destroysded");
            DeselectAll(eventData);
            currentlySelected.Clear();
            Destroy(cursor);
            GameManager.gameManager.bubbles += Part.part.partCost;
                foreach(Transform child in container)
                {
                    Destroy(child.gameObject);
                }
            allSelectable.Clear();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        currentlySelected.Add(this);
        //myImage.color = selectedColor;

        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //myImage.color = unselectedColor;
        //transform.GetChild(0).gameObject.SetActive(false);
        if (currentlySelected.Count > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Rotate()
    {
        cursor.transform.eulerAngles = Vector3.forward * rotationSlider.value * 360;
    }

    public void Scale()
    {
        cursor.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
    }
    public static void DeselectAll(BaseEventData eventData)
    {
        foreach (Dragger selectable in currentlySelected)
        {
            selectable.OnDeselect(eventData);
        }
        currentlySelected.Clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect(eventData);
    }
}
