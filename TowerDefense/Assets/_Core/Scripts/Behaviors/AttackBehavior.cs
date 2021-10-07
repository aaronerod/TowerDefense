using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for Attack behaviors
/// </summary>
public abstract class AttackBehavior : MonoBehaviour, IAttacker
{

    public GameObject GameObject => gameObject;
    protected AttackData attackData;

    [SerializeField]
    float timeToNewAttack;
    public virtual void UpdateAttack(IDamageReceiver target, AttackData attackData)
    {
        this.attackData = attackData;
        timeToNewAttack -= Time.deltaTime;
        if (target != null)
        {
            if (timeToNewAttack <= 0)
            {
                timeToNewAttack = attackData.AttackRate;
                if(Vector2.Distance(target.GameObject.transform.position,transform.position)<=attackData.Range)
                Shoot(target);
            }
        }
    }

    public abstract void Shoot(IDamageReceiver target);
}
