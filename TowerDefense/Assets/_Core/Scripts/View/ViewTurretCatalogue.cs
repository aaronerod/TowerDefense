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
            turretItem.Initialize(turret);
            turretInstance.transform.SetParent(itemsParent, false);
        }
    }


}
