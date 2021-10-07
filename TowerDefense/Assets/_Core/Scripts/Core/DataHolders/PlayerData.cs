using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName ="Player Data", menuName ="TowerDefense/Data Holders/Player Data")]
public class PlayerData : ScriptableObject
{
    private event Action<int> lifesChanged;
    public event Action<int> LifesChanged
    {
        add
        {
            lifesChanged += value;
            if (currentTower != null)
                value(currentTower.Health);
            else
                value(0);
        }
        remove => lifesChanged -= value;
    }
    public event Action TowerDestoryed;
    private event Action<int> levelChanged;
    public event Action<int> LevelChanged
    {
        add
        {
            levelChanged += value;
            value(level);
        }
        remove => levelChanged -= value;
    }
    [SerializeField]
    EconomyData economyData;
    [SerializeField]
    private int level;
    [NonSerialized]
    private Tower currentTower; 
    

    public int Level { get => level; set
        {
            level = value;
            levelChanged?.Invoke(level);
        }
    }

    public Tower CurrentTower { get => currentTower;}
    public EconomyData EconomyData { get => economyData; set => economyData = value; }

    public void SetCurrentTower(Tower tower)
    {
        currentTower = tower;
        currentTower.HealthChanged += OnLifeChanged;
        currentTower.Destroyed += OnTowerDestroyed;
        lifesChanged?.Invoke(currentTower.Health);
    }
    private void OnTowerDestroyed(IDamageReceiver damageReceiver)
    {
        Time.timeScale = 0;
        TowerDestoryed?.Invoke();
    }
    private void OnLifeChanged(IDamageReceiver tower)
    {
        lifesChanged?.Invoke(tower.Health);
    }


    [NaughtyAttributes.Button("Add coins")]
    public void AddCoins()
    {
        EconomyData.AddCoins(5);
    }
}
