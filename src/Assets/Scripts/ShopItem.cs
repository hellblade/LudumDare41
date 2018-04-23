using System;
using UnityEngine.Events;

[Serializable]
public class ShopItem
{
    public string name;
    public int cost;

    public UnityEvent onPurchase = new UnityEvent();

    public void Purchase(InventoryManager inventory)
    {
        if (inventory.Coins < cost)
            return;

        inventory.UseCoins(cost);
        onPurchase.Invoke();
    }
}

