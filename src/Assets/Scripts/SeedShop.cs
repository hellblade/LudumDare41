using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedShop : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] InventoryManager inventory;
    [SerializeField] int cost;

    RunnerManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<RunnerManager>();
        gameManager.GameStarted.AddListener(OnGameStarted);
        gameManager.GameEnded.AddListener(OnGameEnded);
    }


    public void OnGameEnded()
    {
        button.gameObject.SetActive(true);

        button.interactable = (inventory.Coins > 10);
    }

    public void OnGameStarted()
    {
        button.gameObject.SetActive(false);
    }

    public void Purchase()
    {
        if (inventory.Coins < 10)
            return;

        inventory.UseCoins(10);
        inventory.AddSeed();

        button.interactable = (inventory.Coins > 10);
    }
}