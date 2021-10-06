using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class InputDrag : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent DragStarted;
    public UnityEvent DragUpdated;
    public UnityEvent DragCompleted;
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStarted?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragUpdated?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragCompleted?.Invoke();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
    }
}
