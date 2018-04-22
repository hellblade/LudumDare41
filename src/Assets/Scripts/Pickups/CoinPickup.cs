﻿using UnityEngine;
using System.Collections;

public class CoinPickup : Pickup
{
    public static ObjectPool<CoinPickup> Pool = new ObjectPool<CoinPickup>();

    protected override System.Type RequiredComponent
    {
        get
        {
            return typeof(InventoryManager);
        }
    }

    protected override bool OnPickup(Component target)
    {
        var inventory = target as InventoryManager;

        if (!inventory)
            return false;

        inventory.AddCoin();
        this.gameObject.SetActive(false);
        Pool.Free(this);

        return true;
    }
}