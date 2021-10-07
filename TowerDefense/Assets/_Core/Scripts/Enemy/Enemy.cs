using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageReceiver
{
    [SerializeField]
    private MovementBehavior movementBehavior;
    [SerializeField]
    private PlayerData playerData;
    private Transform attackTargetTransform;
    private EnemyData enemyData;
    [SerializeField]
    private AttackBehavior attackBehavior;
    private IDamageReceiver attackTarget;

    private float timeToNewAttack;

    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }

    public void Initialize(EnemyData enemyData, IDamageReceiver attackTarget)
    {
        Initialize(enemyData.Health);
        this.enemyData = enemyData;
        Health = enemyData.Health;
        this.attackTarget = attackTarget;
        attackTargetTransform = attackTarget.GameObject.transform;
        attackTarget = attackTargetTransform.GetComponent<IDamageReceiver>();
        movementBehavior.Initialize(attackTargetTransform, EnemyData.MovementSpeed, EnemyData.AttackData.Range);
    }
    void Start()
    {
        Destroyed += OnDestroyed;
    }
    private void OnDestroyed(IDamageReceiver damageReceiver)
    {
        playerData.EconomyData.AddCoins(enemyData.Coins);
    }

    void Update()
    {
        if (IsAlive)
        {
            movementBehavior.UpdateBehavior();
            attackBehavior.UpdateAttack(attackTarget, enemyData.AttackData);
        }
    }
    void OnDrawGizmosSelected()
    {
        if(enemyData)
            Gizmos.DrawWireSphere(transform.position, EnemyData.AttackData.Range);
    }

}
