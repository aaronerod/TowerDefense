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
    public event Action<IDamageReceiver> HealthChanged;

    protected int health;
    protected int maxHealth;
    public int Health
    {
        get => health;
        set {
            health = value;
            HealthChanged?.Invoke(this);
        }

    }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public GameObject GameObject => gameObject;

    public bool IsAlive => Health>0;


    public void Initialize(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    public void TakeDamage(IAttacker attacker, int amount)
    {
        Health = Mathf.Clamp(Health- amount,0,int.MaxValue);
        if (Health <= 0)
        {
            Destroyed?.Invoke(this);
        }
    }

#if UNITY_EDITOR
    [NaughtyAttributes.Button("Simulate Damage", NaughtyAttributes.EButtonEnableMode.Playmode)]
    void SimulateDamage()
    {
        TakeDamage(null, 1);
    }

#endif
}
