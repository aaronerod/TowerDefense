using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class InputDrag : InputBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEventVector DragStarted;
    public UnityEventVector DragUpdated;
    public UnityEventVector DragCompleted;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 worldPosition = GetWorldPosition(eventData.position);
        DragStarted?.Invoke(worldPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPosition = GetWorldPosition(eventData.position);
        DragUpdated?.Invoke(worldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 worldPosition = GetWorldPosition(eventData.position);
        DragCompleted?.Invoke(worldPosition);
    }

}
