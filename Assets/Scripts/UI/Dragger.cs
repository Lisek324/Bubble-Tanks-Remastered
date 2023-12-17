using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Dragger : MonoBehaviour, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IPointerDownHandler, ISelectHandler, IDeselectHandler, IPointerUpHandler, IPointerClickHandler
{
    public static HashSet<Dragger> allSelectable = new HashSet<Dragger>();
    public static HashSet<Dragger> currentlySelected = new HashSet<Dragger>();
    //add to this list ONLY obcjects that are correctly connected to themselfs;
    public static List<GameObject> connectedParts = new List<GameObject>();
    public bool dragmultiple = false;
    public bool snapToGrid = true;
    public bool xMirror = true;
    public bool isConnected = false;
    public bool isBase = false;
    public float gridSize = 2f;
    public float min_x, max_x, min_y, max_y;
    public GameObject cursor;
    public GameObject selectionBox;
    public GameObject partInventory;

    private GameManager gameManager;
    public static RectTransform container;
    public static RectTransform builder;
    private Slider rotationSlider;
    private Slider scaleSlider;
    public static Canvas canvas;
    const int UI_HEIGHT = 1080;
    public static Vector2 size;
    Color c;
    public void Start()
    {
        gameManager = GameManager.gameManager;
        canvas = GameObject.Find("HUD").GetComponent<Canvas>();

        rotationSlider = GameObject.Find("RotationSlider").GetComponent<Slider>();
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();

        builder = GameObject.Find("PlayerEdit").GetComponent<RectTransform>();
        container = GameObject.Find("Container").GetComponent<RectTransform>();

        partInventory = GameObject.Find("ConstructionZone");


        scaleSlider.onValueChanged.AddListener(delegate
        {
            Scale();
        });

        rotationSlider.onValueChanged.AddListener(delegate
        {
            Rotate();
        });
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (dragmultiple == false)
        {
            if (gameObject.transform.parent.name == "Background")
            {
                cursor = Instantiate(gameObject, transform, false);
                cursor.name = cursor.name.Substring(0, cursor.name.Length - 7);
                cursor.transform.SetParent(builder, false);
                gameManager.bubbles -= cursor.GetComponent<Part>().partCost;
            }
            else
            {
                cursor = gameObject;
                rotationSlider.value = cursor.transform.eulerAngles.z / 360;
            }
        }

        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            DeselectAll(eventData);
        }
        else
        {
            cursor.transform.SetParent(container);
        }
        c = cursor.GetComponent<Image>().color;
        cursor.transform.SetAsLastSibling();

        connectedParts.Clear();
        foreach (Transform child in builder)
        {
            connectedParts.Add(child.gameObject);
        }
        if (connectedParts.Count == 1 && !cursor.name.Contains("(Base)"))
        {
            //connectedParts[0].name = connectedParts[0].name + "(Base)";
            connectedParts[0].GetComponent<Dragger>().isConnected = true;
            connectedParts[0].GetComponent<Dragger>().isBase = true;
        }
        /*for (int j = 0; j < connectedParts.Count - 1; j++)
        {
            if (!connectedParts[j].name.Contains("(Base)"))
            {
                continue;
            }
            if (j > connectedParts.Count - 1) 
            {
                Debug.Log(j);
                baseExists = false;
            }
            
        }*/
    }

    public void OnDrag(PointerEventData eventData)
    {
        for (int j = 0; j < connectedParts.Count - 1; j++)
        {
            ///check if two images are overlaping
            if (cursor.GetComponent<RectTransform>().Overlaps(connectedParts[j].GetComponent<RectTransform>()))
            {
                
                if (connectedParts[j].name.Contains("(Base)"))
                {
                    cursor.GetComponent<Dragger>().isConnected = true;
                    c.a = 1f;
                    cursor.GetComponent<Image>().color = c;
                    break;
                }
                else if (connectedParts[j].GetComponent<Dragger>().isConnected == true)
                {
                    cursor.GetComponent<Dragger>().isConnected = true;
                    c.a = 1f;
                    cursor.GetComponent<Image>().color = c;
                    break;
                }
                ///check if part is overlaping connected image and make it 
                if (cursor.GetComponent<Dragger>().isBase)
                {
                    c.a = 1f;
                    connectedParts[j].GetComponent<Image>().color = c;
                    connectedParts[j].GetComponent<Dragger>().isConnected = true;
                }
            }
            else
            {
                if (cursor.GetComponent<Dragger>().isBase)
                {
                    c.a = 0.6f;
                    connectedParts[j].GetComponent<Image>().color = c;

                    connectedParts[j].GetComponent<Dragger>().isConnected = false;
                }
                //if connected parts does not contain an gameobject with (base) name with it, make all connectedparts with colored alpha 0.6
                cursor.GetComponent<Dragger>().isConnected = false;
                c.a = 0.6f;
                cursor.GetComponent<Image>().color = c;
            }
        }

        if (currentlySelected.Count >= 2)
        {
            foreach (Transform child in builder)
            {
                foreach (Transform child2 in child)
                {
                    if (child2.gameObject.activeSelf.Equals(true))
                    {
                        child.transform.SetParent(container);
                    }
                }
            }
            dragmultiple = true;
        }
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
        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)partInventory.transform, cursor.transform.position))
        {
            Destroy(cursor.GetComponent<TooltipTrigger>());
        }
        foreach (Transform selectables in builder)
        {
            allSelectable.Add(selectables.GetComponent<Dragger>());
        }
        
        dragmultiple = false;
        cursor.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cursor.transform.GetChild(0).gameObject.SetActive(true);
        currentlySelected.Add(cursor.GetComponent<Dragger>());
        Debug.Log(currentlySelected.Count);
        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform)partInventory.transform, cursor.transform.position))
        {
            DeselectAll(eventData);
            currentlySelected.Clear();
            Destroy(cursor);
            allSelectable.Remove(cursor.GetComponent<Dragger>());
                foreach (Transform child in container)
                {
                    Destroy(child.gameObject);
                    allSelectable.Remove(child.GetComponent<Dragger>());
                    gameManager.bubbles += child.GetComponent<Part>().partCost;
                }
            if (container.childCount == 0) gameManager.bubbles += cursor.GetComponent<Part>().partCost;
        }
        gameManager.scoreText.text = "Bubbles: " + gameManager.bubbles.ToString();
        //allSelectable.Clear();
    }

    public void OnSelect(BaseEventData eventData)
    {
        currentlySelected.Add(this);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (currentlySelected.Count > 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Rotate()
    {   
        foreach (Dragger child in currentlySelected)
        {
            child.transform.eulerAngles = Vector3.forward * rotationSlider.value * 360;
        }
    }

    public void Scale()
    {
        foreach (Dragger child in currentlySelected)
        {
            child.transform.localScale = new Vector3(scaleSlider.value, scaleSlider.value, scaleSlider.value);
        }

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
public static class RectTransformExtensions
{

    public static bool Overlaps(this RectTransform a, RectTransform b)
    {
        return a.WorldRect().Overlaps(b.WorldRect());
    }
    public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
    {
        return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
    }

    public static Rect WorldRect(this RectTransform rectTransform)
    {
        Vector2 sizeDelta = rectTransform.sizeDelta;
        float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
        float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

        Vector3 position = rectTransform.position;
        return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
    }
}

