using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewDamagableHealth : MonoBehaviour
{
    [SerializeField]
    private DamageReceiver damageReceiver;
    [SerializeField]
    Image healthBar;
    [SerializeField]
    Vector3 offset;

    private void Start()
    {
        damageReceiver.HealthChanged += OnUpdateHealth;
    }
    private void Update()
    {
        transform.position = damageReceiver.transform.position + offset;
        transform.rotation = Quaternion.identity;
    }
    private void OnEnable()
    {
        OnUpdateHealth(damageReceiver);
    }
    void OnUpdateHealth(IDamageReceiver damageReceiver)
    {
        if (damageReceiver.MaxHealth == 0)
            healthBar.fillAmount = 1;
        else
            healthBar.fillAmount = (float)damageReceiver.Health /(float) damageReceiver.MaxHealth;
    }
}
