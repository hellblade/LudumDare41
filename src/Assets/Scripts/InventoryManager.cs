using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private const string CoinPref = "Coins";

    public int Coins { get; private set; }

    void Awake()
    {
        Coins = PlayerPrefs.GetInt(CoinPref);
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt(CoinPref, Coins);
        PlayerPrefs.Save();
    }

    public void AddCoin()
    {
        Coins++;
        SavePrefs();
    }

}