using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewTurretCatalogueItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtCost;
    [SerializeField]
    private RectTransform iconParent;

    private GameObject iconInstance;
    public void Initialize(TurretData turretData)
    {
        if (iconInstance != null)
            Destroy(iconInstance);
        if (turretData.UiTurretPrefab)
        {
            iconInstance = Instantiate(turretData.UiTurretPrefab);
            iconInstance.transform.SetParent(iconParent, false);
        }
        txtCost.text = turretData.Cost.ToString();
    }

    void OnCoinsUpdated()
    {

    }
}
