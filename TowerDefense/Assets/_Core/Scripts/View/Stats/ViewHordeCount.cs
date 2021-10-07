using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewHordeCount : MonoBehaviour
{
    private const string Horde = "Horde ";
    [SerializeField]
    private HordeController hordeController;
    [SerializeField]
    private TextMeshProUGUI txtHorde;
    private void Start()
    {
        hordeController.CurrentHordeChanged += OnHordeChanged;
    }
    void OnHordeChanged(int horde)
    {
        txtHorde.text = Horde + horde + "/" + hordeController.TotalHordes;
    }
}
