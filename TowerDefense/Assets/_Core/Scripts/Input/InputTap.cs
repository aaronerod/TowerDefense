using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class InputTap : InputBase, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEventVector PointerDown;
    public UnityEventVector PointerUp;
    public UnityEventVector PointerClick;
    [SerializeField]
    private float clickThreshold=1;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.position, eventData.pressPosition) <= clickThreshold)
        {
            Vector3 worldPosition = GetWorldPosition(eventData.position);
            PointerClick?.Invoke(worldPosition);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPosition = GetWorldPosition(eventData.position);
        PointerDown?.Invoke(worldPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 worldPosition = GetWorldPosition(eventData.position);
        PointerUp?.Invoke(worldPosition);
    }
}
