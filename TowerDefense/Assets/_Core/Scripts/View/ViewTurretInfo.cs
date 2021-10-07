using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ViewTurretInfo : MonoBehaviour
{
    [SerializeField]
    private MapControllerConnector mapControllerConnector;
    [SerializeField]
    private Transform range;
    [SerializeField]
    private TextMeshProUGUI txtCost;

    private TurretData turretData;
    private Turret turret;
    public void Initialize(TurretData turretData, IBuildable buildable)
    {
        Turret turret = buildable.GameObject.GetComponent<Turret>();
        if (turret != null)
        {
            transform.position = turret.transform.position;
                txtCost.text = turretData.SellingCost.ToString();
            range.transform.localScale = Vector3.one * turretData.AttackData.Range*2;
            this.turret = turret;
            this.turretData = turretData;
        }
    }
    public void OnSellTurret()
    {
        mapControllerConnector.Controller.SellTurret(turret);
        gameObject.SetActive(false);
    }
}
