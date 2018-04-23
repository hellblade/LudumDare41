using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const string CoinPref = "Coins";
    private const string SeedsPref = "Seeds";

    public int Coins { get; private set; }

    public int Seeds { get; private set; }

    void Awake()
    {
        Coins = PlayerPrefs.GetInt(CoinPref);
        Seeds = PlayerPrefs.GetInt(SeedsPref);
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt(CoinPref, Coins);
        PlayerPrefs.SetInt(SeedsPref, Seeds);
        PlayerPrefs.Save();
    }

    public void AddCoin()
    {
        Coins++;
        SavePrefs();
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
        SavePrefs();
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
        SavePrefs();
    }

    public void AddSeed()
    {
        Seeds++;
        SavePrefs();
    }

    public void UseSeeds(int amount)
    {
        Seeds -= amount;
        SavePrefs();
    }

}