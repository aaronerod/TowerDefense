using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Contains the currency of the player
/// </summary>
[Serializable]
public class EconomyData
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
    [SerializeField]
    private int coins;

    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            coinsChanged?.Invoke(coins);
        }
    }

    /// <summary>
    /// Verifies if there is enough coins for the given amount
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool EnoughCoins(int amount)
    {
        return Coins >= amount;
    }

    /// <summary>
    /// Spend coins
    /// </summary>
    /// <param name="amount">The amount of coins to spend</param>
    /// <returns>True when the purchase is successful</returns>
    public bool SpendCoins(int amount)
    {
        bool result = false;
        if (EnoughCoins(amount))
        {
            Coins -= amount;
            result = true;
        }
        return result;
    }

    /// <summary>
    /// Increment coins
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoins(int amount)
    {
        Coins += amount;
    }
}
