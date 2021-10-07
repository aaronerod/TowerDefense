using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class UnityEventVector : UnityEvent<Vector3> { }

public abstract class InputBase : MonoBehaviour
{
    [SerializeField]
    protected Camera mainCamera;
    public void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }
}
