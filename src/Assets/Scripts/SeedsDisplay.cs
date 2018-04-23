using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedsDisplay : MonoBehaviour
{
    Text label;

    InventoryManager inventory;

    void Awake()
    {
        inventory = FindObjectOfType<InventoryManager>();

        label = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        label.text = inventory.Seeds.ToString();
    }
}