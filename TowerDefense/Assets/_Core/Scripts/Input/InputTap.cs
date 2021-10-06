using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
[System.Serializable]
public class UnityEventVector : UnityEvent<Vector3> { }
public class InputTap : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public UnityEventVector PointerDown;
    public UnityEventVector PointerUp;
    public UnityEventVector PointerClick;
    [SerializeField]
    private float clickThreshold=1;

    [SerializeField]
    private Camera mainCamera;
    public void Awake()
    {
        if(mainCamera==null)
        mainCamera = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Vector2.Distance(eventData.position, eventData.pressPosition) <= clickThreshold)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
            PointerClick?.Invoke(worldPosition);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        PointerDown?.Invoke(worldPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        PointerUp?.Invoke(worldPosition);
    }
}
