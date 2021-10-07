using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Dictionary<EnemyData, Stack<GameObject>> destroyedEnemies = new Dictionary<EnemyData, Stack<GameObject>>();
    [SerializeField]
    Transform target;
    [SerializeField]
    private HordeController hordeController;
    public void Initialize(HordeController hordeController)
    {
        this.hordeController = hordeController;
    }
    public Enemy Spawn(EnemyData enemyData, Vector2 worldPosition)
    {

        GameObject enemyInstance = GetEnemy(enemyData);
        enemyInstance.transform.rotation = Quaternion.identity;
        enemyInstance.transform.position = worldPosition;
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialize(enemyData);
        enemy.attackTargetTransform = target;

        SetUpEnemy(enemy);
        return enemyInstance.GetComponent<Enemy>();
    }
    public void SetUpEnemy(Enemy enemy)
    {
        enemy.Destroyed += OnEnemyDestroyed;
    }

    public void OnEnemyDestroyed(IDamageReceiver damageReceiver)
    {
        Enemy enemy = damageReceiver as Enemy;
        enemy.Destroyed -= OnEnemyDestroyed;

        Stack<GameObject> enemies;
        if (destroyedEnemies.TryGetValue(enemy.EnemyData, out enemies))
        {
            enemies.Push(enemy.gameObject);
        }
        else
        {
            enemies = new Stack<GameObject>();
            enemies.Push(enemy.gameObject);
            destroyedEnemies.Add(enemy.EnemyData, enemies);
        }
        enemy.GameObject.SetActive(false);
    }

    public GameObject GetEnemy(EnemyData enemyData)
    {
        Stack<GameObject> enemies;
        if (destroyedEnemies.TryGetValue(enemyData, out enemies))
        {
            if (enemies.Count > 0)
            {
                GameObject enemy = enemies.Pop();
                enemy.SetActive(true);
                return enemy;
            }

        }
        GameObject enemyInstance = Instantiate(enemyData.EnemyPrefab);
        return enemyInstance;
    }

#if UNITY_EDITOR
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private Transform spawnPosition;
    [NaughtyAttributes.Button("Test Spawn")]
    public void TestSpawn()
    {
        Spawn(enemyData, spawnPosition.position);
    }
#endif
}
