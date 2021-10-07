using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all turrets
/// </summary>
public class Turret : MonoBehaviour, IAttacker, IBuildable
{
    [SerializeField]
    private TurretData turretData;
    public GameObject GameObject => gameObject;

    private Transform transformTarget;
    [SerializeField]
    private IDamageReceiver target;

    [SerializeField]
    private Transform turret;
    [SerializeField]
    private float rotationSpeed = .5f;


    [SerializeField]
    float timeToNewAttack;

    public void Initialize(TurretData turretData)
    {
        this.turretData = turretData;
    }

    public void UpdateBehaviors(List<Enemy> enemies)
    {
        UpdateTarget(enemies);
        Attack();
    }

    void SearchNearestTarget(List<Enemy> enemies)
    {
        Enemy temporalTarget = null;
        float minDistance = float.MaxValue;
        foreach(var enemy in enemies)
        {
            if (enemy.IsAlive)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    temporalTarget = enemy;
                    minDistance = distance;
                }
            }
        }
        
        target = temporalTarget;
        if(target!=null)    
        transformTarget = temporalTarget.transform;
    }
    public void UpdateTarget(List<Enemy> enemies)
    {
        SearchNearestTarget(enemies);
        if (transformTarget != null)
        {
            if (Vector2.Distance(transform.position, transformTarget.position) <= turretData.AttackData.Range)
            {
                Vector3 desiredDirection = transformTarget.position - transform.position;
                turret.up += desiredDirection;
            }
            else
            {
                transformTarget = null;
                target = null;
            }
        }
    }

    public void Attack()
    {
        timeToNewAttack -= Time.deltaTime;
        if (target != null)
        {
            if (timeToNewAttack <= 0 )
            {
                timeToNewAttack = turretData.AttackData.AttackRate;
                target.TakeDamage(this, turretData.AttackData.DamageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(turretData)
        Gizmos.DrawWireSphere(transform.position, turretData.AttackData.Range);
    }

    public void Build(GridCell gridCell)
    {
        throw new System.NotImplementedException();
    }
}
