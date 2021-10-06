using UnityEngine;

/// <summary>
/// Interface for objects that can take damage
/// </summary>
public interface IDamageReceiver:IGameObject
{
    /// <summary>
    /// Notifies when the object has been destroyed
    /// </summary>
    public event System.Action<IDamageReceiver> Destroyed;
    public event System.Action<IDamageReceiver> DamageReceived;
    /// <summary>
    /// Current Health
    /// </summary>
    public int Health { get; set; }
    public bool IsAlive { get; }
    /// <summary>
    /// 
    /// Apply the amount of damage
    /// </summary>
    /// <param name="attacker">Object who is attacking</param>
    /// <param name="amount">Damage to apply</param>
    public void TakeDamage(IAttacker attacker, int amount);
}
