using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all turrets
/// </summary>
public class Turret : MonoBehaviour, IBuildable
{
    [SerializeField]
    private TurretData turretData;
    public GameObject GameObject => gameObject;

    public TurretData TurretData => turretData;

    private Transform transformTarget;
    [SerializeField]
    private IDamageReceiver target;

    [SerializeField]
    private Transform turret;
    [SerializeField]
    private AttackBehavior attackBehavior;
    [SerializeField]
    private float rotationSpeed = .5f;



    public void Initialize(TurretData turretData)
    {
        this.turretData = turretData;
        transformTarget = null;
        target = null;
    }

    public void UpdateBehaviors(List<Enemy> enemies)
    {
        UpdateTarget(enemies);
        attackBehavior.UpdateAttack(target, turretData.AttackData);
    }

    public void UpdateTarget(List<Enemy> enemies)
    {
        if (target != null && !target.IsAlive ||
            transformTarget == null || (transformTarget != null &&
            Vector2.Distance(transform.position, transformTarget.position) > turretData.AttackData.Range) ||
            transformTarget!=null && !transformTarget.gameObject.activeInHierarchy)
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


    void SearchNearestTarget(List<Enemy> enemies)
    {
        Enemy temporalTarget = null;
        float minDistance = float.MaxValue;
        foreach (var enemy in enemies)
        {
            if (enemy.IsAlive && turretData.AttackData.TargetType.HasFlag(enemy.EnemyData.AttackData.UnitType))
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
        if (target != null)
            transformTarget = temporalTarget.transform;
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
