using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewTurret : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteBase;
    [SerializeField]
    private Transform range;
    [SerializeField]
    Transform turretParent;
    [SerializeField]
    private Color availableColor;
    [SerializeField]
    private Color notAvailableColor;
    [SerializeField]
    private MapControllerConnector mapConnector;
    [SerializeField]
    private InputDrag inputDrag;


    private TurretData currentTurret;
    private GameObject turretInstance;

    public void Initialize(TurretData turretData, Vector2 position)
    {
        currentTurret = turretData;
        range.localScale = Vector3.one * turretData.AttackData.Range * 2;
        if (turretInstance != null)
            Destroy(turretInstance);
        turretInstance = Instantiate(turretData.TurretPrefab);
        turretInstance.transform.SetParent(turretParent,false);
        turretInstance.transform.localPosition = Vector3.zero;
        spriteBase.transform.localScale = Vector3.one * mapConnector.Controller.GridMap.CellSize;
        OnPositionUpdated(position);
    }

    private void OnEnable()
    {
        inputDrag.DragUpdated.AddListener(OnPositionUpdated);

    }
    private void OnDisable()
    {
        inputDrag.DragUpdated.RemoveListener(OnPositionUpdated);
    }

    public void OnPositionUpdated(Vector3 worldPosition)
    {
        GridCell currentCell = mapConnector.Controller.GridMap.GetNearestCell(worldPosition);
        if (currentCell != null)
        {
            transform.position = currentCell.WorldCoordinates;
            if (currentCell.IsEmpty && (mapConnector.Controller.AllowBuildOverWalkable || !currentCell.IsWalkable))
                spriteBase.color = availableColor;
            else
                spriteBase.color = notAvailableColor;
        }
    }

    /// <summary>
    /// Invoked from the editor
    /// </summary>
    public void OnPurchaseTurret()
    {
        mapConnector.Controller.PurchaseSelectedTurret(transform.position);
    }

    public void OnCancelPurchase()
    {
        gameObject.SetActive(false);
    }
}
