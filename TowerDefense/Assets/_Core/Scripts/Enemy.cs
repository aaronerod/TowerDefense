using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageReceiver, IAttacker
{
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    public Transform attackTargetTransform;
    [SerializeField]
    private MovementBehavior movementBehavior;
    private IDamageReceiver attackTarget;

    private float timeToNewAttack;

    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }

    public void Initialize(EnemyData enemyData)
    {
        this.enemyData = enemyData;
        Health = enemyData.Health;
    }
    void Start()
    {
        Destroyed += OnDestroyed;
        attackTarget = attackTargetTransform.GetComponent<IDamageReceiver>();
        movementBehavior.Initialize(attackTargetTransform, EnemyData.MovementSpeed, EnemyData.AttackData.Range);
    }
    private void OnDestroyed(IDamageReceiver damageReceiver)
    {
        Debug.LogError("Enemy destroyed");
    }

    

    void Update()
    {
        if (IsAlive)
        {
            movementBehavior.UpdateBehavior();
            Attack();
        }
    }
    public void Attack()
    {
        timeToNewAttack -= Time.deltaTime;
        if (attackTarget != null)
        {
            if (timeToNewAttack <= 0 && Vector2.Distance(attackTargetTransform.position, transform.position) <= EnemyData.AttackData.Range)
            {
                timeToNewAttack = EnemyData.AttackData.AttackRate;
                attackTarget.TakeDamage(this, EnemyData.AttackData.DamageAmount);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if(enemyData)
            Gizmos.DrawWireSphere(transform.position, EnemyData.AttackData.Range);
    }

}
