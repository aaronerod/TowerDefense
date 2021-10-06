using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for objects that can be destroyed
/// </summary>
public abstract class DamageReceiver : MonoBehaviour, IDamageReceiver
{
    public event Action<IDamageReceiver> Destroyed;
    public event Action<IDamageReceiver> DamageReceived;

    [SerializeField]
    protected int health;
    public int Health { get => health; set =>health = value; }
    public GameObject GameObject => gameObject;

    public bool IsAlive => health>0;


    public void TakeDamage(IAttacker attacker, int amount)
    {
        health = Mathf.Clamp(health- amount,0,int.MaxValue);
        if (health <= 0)
        {
            Destroyed?.Invoke(this);
        }
        else
            DamageReceived?.Invoke(this);
    }

#if UNITY_EDITOR
    [NaughtyAttributes.Button("Simulate Damage", NaughtyAttributes.EButtonEnableMode.Playmode)]
    void SimulateDamage()
    {
        TakeDamage(null, 1);
    }
#endif
}
