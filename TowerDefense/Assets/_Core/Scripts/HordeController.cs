using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    public List<Enemy> activeEnemies = new List<Enemy>();
    [SerializeField]
    private HordeData hordeData;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private Transform spawnPosition;

    private void Start()
    {
        enemySpawner.Initialize(this);
    }
    [NaughtyAttributes.Button("Start Horde")]
    public void StartHorde()
    {
        StartCoroutine(SpawnHorde());
    }

    private IEnumerator SpawnHorde()
    {
        float elapsed = 0;
        int currentGroup = 0;
        Horde horde = hordeData.Hordes[0];
        int amount = 0;
        while (currentGroup < horde.Groups.Count)
        {
            elapsed += Time.deltaTime;
           
            HordeGroup hordeGroup = horde.Groups[currentGroup];
            if (elapsed >= hordeGroup.SpawnRate)
            {
                for (int i = 0; i < hordeGroup.Enemies.Count; i++)
                {
                    Enemy enemy = enemySpawner.Spawn(hordeGroup.Enemies[i], spawnPosition.position);
                    SetUpEnemy(enemy);
                }
                amount++;
                elapsed = 0;
            }
            if (amount >= hordeGroup.SpawnAmount)
                currentGroup++;
            yield return null;
        }
        Debug.LogError("Horde completed");
    }

    void SetUpEnemy(Enemy enemy)
    {
        enemy.Destroyed += OnEnemyDestroyed;
        activeEnemies.Add(enemy);
    }

    void OnEnemyDestroyed(IDamageReceiver damageReceiver)
    {
        Enemy enemy = damageReceiver as Enemy;
        enemy.Destroyed -= OnEnemyDestroyed;
        activeEnemies.Remove(enemy);
    }
}
