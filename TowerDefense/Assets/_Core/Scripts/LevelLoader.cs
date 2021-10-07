using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public event System.Action LevelLoaded;
    [SerializeField]
    private LevelsCollection levelsCollection;
    [SerializeField]
    PlayerData playerData;
    private LevelData currentLevel;
    [SerializeField]
    private Tower tower;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private MapController mapController;
    [SerializeField]
    private HordeController hordeSpawner;
    public LevelData CurrentLevel { get => currentLevel; set => currentLevel = value; }


    public void Restart()
    {
        LoadLevel(currentLevel);
    }
    public void LoadLevel(LevelData levelData)
    {
        this.CurrentLevel = levelData;
        tower.Initialize(CurrentLevel.TowerLife);
        playerData.EconomyData.Coins = CurrentLevel.InitialCoins;
        playerData.SetCurrentTower(tower);

        Vector3 towerPoint;
        Vector3 spawnPoint;
        mapController.Initialize(CurrentLevel, out towerPoint, out spawnPoint);
        tower.transform.position = towerPoint;
        spawnPosition.position = spawnPoint;
        hordeSpawner.Initialize(CurrentLevel.HordeData, tower, spawnPosition.position);
        LevelLoaded?.Invoke();
    }


    public void LoadNextLevel()
    {
        LoadLevel(levelsCollection.NextLevel(currentLevel));
    }
}
