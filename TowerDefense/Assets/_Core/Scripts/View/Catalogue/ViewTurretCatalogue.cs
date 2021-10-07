using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewTurretCatalogue : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;
    [SerializeField]
    private GameObject catalogItemPrefab;
    [SerializeField]
    private Transform itemsParent;
    [SerializeField]
    private MapControllerConnector mapControllerConnector;

    private void Start()
    {
        LoadCatalogue();
    }

    private void LoadCatalogue()
    {
        foreach(var turret in levelData.AvailableTurrets)
        {
            GameObject turretInstance = Instantiate(catalogItemPrefab);
            ViewTurretCatalogueItem turretItem = turretInstance.GetComponent<ViewTurretCatalogueItem>();
            turretItem.Initialize(this, turret);
            turretInstance.transform.SetParent(itemsParent, false);
        }
    }

    public void SelectTurret(TurretData turretData)
    {
        mapControllerConnector.Controller.PreviewTurret(turretData);
    }

}
