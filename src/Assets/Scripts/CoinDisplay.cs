using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    Text label;
    string text;

    InventoryManager inventory;

    void Awake()
    {
        inventory = FindObjectOfType<InventoryManager>();

        label = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        label.text = inventory.Coins.ToString();
    }
}