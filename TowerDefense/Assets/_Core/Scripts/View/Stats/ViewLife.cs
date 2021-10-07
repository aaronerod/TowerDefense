using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewLife : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtLifes;
    [SerializeField]
    private PlayerData playerData;

    private void OnEnable()
    {
        playerData.LifesChanged += OnLifesUpdated;
    }
    private void OnDisable()
    {
        playerData.LifesChanged -= OnLifesUpdated;
    }

    private void OnLifesUpdated(int lifes)
    {
        txtLifes.text = lifes.ToString();
    }
}
