using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    public event System.Action LevelCompleted;
    public event System.Action<int> CurrentHordeChanged;
    public event System.Action HordeCompleted;
    public List<Enemy> activeEnemies = new List<Enemy>();
    private HordeData hordeData;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private Transform spawnPosition;

    private int currentHorde;
    private int groupsSpawning;
    private bool hordesCompleted;
    private bool isPlaying;
    public int CurrentHorde
    {
        get => currentHorde; set
        {
            currentHorde = value;
            CurrentHordeChanged?.Invoke(currentHorde);
        }
    }
    public int TotalHordes => hordeData.Hordes.Count;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    public void Initialize(HordeData hordeData, Tower towerTarget, Vector2 spawnPosition)
    {
        isPlaying = false;
        StopAllCoroutines();
        List<Enemy> enemiesCopy = new List<Enemy>(activeEnemies);
        foreach(var enemy in enemiesCopy)
        {
            OnEnemyDestroyed(enemy);
            enemySpawner.OnEnemyDestroyed(enemy);
        }
        this.hordeData = hordeData;
        this.spawnPosition.position = spawnPosition;
        enemySpawner.Initialize(this, towerTarget);
        CurrentHorde = 0;
        IsPlaying = false;
        hordesCompleted = false;
        groupsSpawning = 0;

    }
    [NaughtyAttributes.Button("Start Horde")]
    public void StartHorde()
    {
        StartCoroutine(PlayHordes());
        IsPlaying = true;
    }

    public IEnumerator PlayHordes()
    {
        float elapsed = hordeData.DelayBetweenHordes;
        while (currentHorde < hordeData.Hordes.Count )
        {
            elapsed += Time.deltaTime;
            Horde horde = hordeData.Hordes[currentHorde];
            if (elapsed >= hordeData.DelayBetweenHordes || groupsSpawning <= 0 && activeEnemies.Count == 0)
            {
                for (int i = 0; i < horde.Groups.Count; i++)
                {
                    StartCoroutine(SpawnHordeGroup(horde.Groups[i]));
                    groupsSpawning++;
                }
                elapsed = 0;
                CurrentHorde++;
            }
            yield return null;
        }
        hordesCompleted = true;
    }


    public IEnumerator SpawnHordeGroup(HordeGroup hordeGroup)
    {
        float elapsed = 0;
        int amount = 0;
        while (amount < hordeGroup.SpawnAmount)
        {
            elapsed += Time.deltaTime;

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
            yield return null;
        }
        groupsSpawning--;
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
        if (hordesCompleted && activeEnemies.Count == 0 && isPlaying)
        {
            Debug.LogError("Level completed");
            LevelCompleted?.Invoke();
        }
    }
}
