using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragSelectionHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]Image selectionBoxImage;

    Vector2 startPosition;
    Rect selectionRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
        {
            Dragger.DeselectAll(new BaseEventData(EventSystem.current));
        }
        selectionBoxImage.gameObject.SetActive(true);
        startPosition = eventData.position;
        selectionRect = new Rect();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.position.x < startPosition.x)
        {
            selectionRect.xMin = eventData.position.x / Dragger.canvas.scaleFactor;
            selectionRect.xMax = startPosition.x / Dragger.canvas.scaleFactor;
        }
        else
        {
            selectionRect.xMin = startPosition.x / Dragger.canvas.scaleFactor;
            selectionRect.xMax = eventData.position.x / Dragger.canvas.scaleFactor;
        }

        if (eventData.position.y < startPosition.y)
        {
            selectionRect.yMin = eventData.position.y / Dragger.canvas.scaleFactor;
            selectionRect.yMax = startPosition.y / Dragger.canvas.scaleFactor;
        }
        else
        {
            selectionRect.yMin = startPosition.y / Dragger.canvas.scaleFactor;
            selectionRect.yMax = eventData.position.y / Dragger.canvas.scaleFactor;
        }

        selectionBoxImage.rectTransform.offsetMin = selectionRect.min;
        selectionBoxImage.rectTransform.offsetMax = selectionRect.max;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        selectionBoxImage.gameObject.SetActive(false);
        foreach (Dragger selectable in Dragger.allSelectable)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)selectionBoxImage.transform, selectable.transform.position))
            {
                selectable.OnSelect(eventData);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        float myDistance = 0;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == gameObject)
            {
                myDistance = result.distance;
            }
        }

        GameObject nextObject = null;
        float maxDistance = Mathf.Infinity;

        foreach (RaycastResult result in results)
        {
            if (result.distance > myDistance && result.distance < maxDistance)
            {
                nextObject = result.gameObject;
                maxDistance = result.distance;
            }
        }
        if (nextObject)
        {
            ExecuteEvents.Execute<IPointerClickHandler>(nextObject, eventData, (x, y) => { x.OnPointerClick((PointerEventData)y); });
        }
    }

}
