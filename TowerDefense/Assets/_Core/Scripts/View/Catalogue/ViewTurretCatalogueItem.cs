using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ViewTurretCatalogueItem : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private TextMeshProUGUI txtCost;
    [SerializeField]
    private RectTransform iconParent;
    [SerializeField]
    private Image costImage;
    [SerializeField]
    private Color enoughCoinsColor;
    [SerializeField]
    private Color notEnoughCoinsColor;

    private GameObject iconInstance;
    private TurretData currentTurretData;
    private ViewTurretCatalogue viewCatalogue;
    

    
    public void Initialize(ViewTurretCatalogue catalogue, TurretData turretData)
    {
        currentTurretData = turretData;
        viewCatalogue = catalogue;
        if (iconInstance != null)
            Destroy(iconInstance);
        if (turretData.UiTurretPrefab)
        {
            iconInstance = Instantiate(turretData.UiTurretPrefab);
            iconInstance.transform.SetParent(iconParent, false);
        }
        txtCost.text = turretData.Cost.ToString();
        OnCoinsUpdated(playerData.EconomyData.Coins);
    }

    private void OnEnable()
    {
        playerData.EconomyData.CoinsChanged += OnCoinsUpdated;
    }

    private void OnDisable()
    {
        playerData.EconomyData.CoinsChanged -= OnCoinsUpdated;
    }


    /// <summary>
    /// Invoked from editor
    /// </summary>
    public void OnSelectTurret()
    {
        viewCatalogue.SelectTurret(currentTurretData);
    }

    void OnCoinsUpdated(int coins)
    {
        if (currentTurretData != null)
        {
            if (playerData.EconomyData.EnoughCoins(currentTurretData.Cost))
                costImage.color = enoughCoinsColor;
            else
                costImage.color = notEnoughCoinsColor;
        }
    }
}
