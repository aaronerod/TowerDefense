using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class ViewCoins : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtCoins;
    [SerializeField]
    PlayerData playerData;
    private void OnEnable()
    {
        playerData.CoinsChanged += OnCoinsUpdated;
        
    }
    private void OnDisable()
    {
        playerData.CoinsChanged -= OnCoinsUpdated;
    }
    private void OnCoinsUpdated(int coins)
    {
        txtCoins.text = coins.ToString();
    }
}
