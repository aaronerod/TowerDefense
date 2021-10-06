using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DamageReceiver, IAttacker
{
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    public Transform transformTarget;
    private IDamageReceiver target;

    private float timeToNewAttack;

    public EnemyData EnemyData { get => enemyData; set => enemyData = value; }

    public void Initialize()
    {
        Health = enemyData.Health;
    }
    void Start()
    {
        Destroyed += OnDestroyed;
        target = transformTarget.GetComponent<IDamageReceiver>();
    }
    private void OnDestroyed(IDamageReceiver damageReceiver)
    {
        Debug.LogError("Enemy destroyed");
    }

    

    void Update()
    {
        if (IsAlive)
        {
            Move();
            Attack();
        }
    }
    public void Move()
    {
        if (Vector2.Distance(transform.position, transformTarget.position) > EnemyData.AttackData.Range)
        {
            Vector3 desiredVelocity = transformTarget.transform.position - transform.position;
            desiredVelocity.Normalize();
            desiredVelocity *= EnemyData.MovementSpeed;
            Vector3 steeringVelocity = desiredVelocity - transform.up;
            transform.up = steeringVelocity;
            transform.position += steeringVelocity * Time.deltaTime;
        }
    }
    public void Attack()
    {
        timeToNewAttack -= Time.deltaTime;
        if (target != null)
        {
            if (timeToNewAttack <= 0 && Vector2.Distance(transformTarget.position, transform.position) <= EnemyData.AttackData.Range)
            {
                timeToNewAttack = EnemyData.AttackData.AttackRate;
                target.TakeDamage(this, EnemyData.AttackData.DamageAmount);
                Debug.LogError("Attack");
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, EnemyData.AttackData.Range);
    }

}
