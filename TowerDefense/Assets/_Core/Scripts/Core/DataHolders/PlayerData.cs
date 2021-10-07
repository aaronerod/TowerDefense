using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName ="Player Data", menuName ="TowerDefense/Data Holders/Player Data")]
public class PlayerData : ScriptableObject
{
    private event Action<int> coinsChanged;
    public event Action<int> CoinsChanged
    {
        add
        {
            coinsChanged += value;
            value(coins);
        }
        remove => coinsChanged -= value;
    }
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
    private int coins;
    [SerializeField]
    private int level;
    [NonSerialized]
    private Tower currentTower; 
    

    public int Coins { get => coins; set
        {
            coins = value;
            coinsChanged?.Invoke(coins);
        }
    }
    public int Level { get => level; set
        {
            level = value;
            levelChanged?.Invoke(level);
        }
    }

    public Tower CurrentTower { get => currentTower; set => currentTower = value; }

    public void SetCurrentTower(Tower tower)
    {
        currentTower = tower;
        currentTower.DamageReceived += OnLifeChanged;
        lifesChanged?.Invoke(currentTower.Health);
    }

    private void OnLifeChanged(IDamageReceiver tower)
    {
        lifesChanged?.Invoke(tower.Health);
    }


    [NaughtyAttributes.Button("Add coins")]
    public void AddCoins()
    {
        Coins += 5;
    }
}
