using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDataManager
{
    private List<InventoryItem> _inventory = new List<InventoryItem>();
    public event Action<List<InventoryItem>> OnInventoryUpdated;

    // Passive bonuses
    public float MoveSpeedBonus { get; private set; }
    public float MinigameMoveSpeedBonus { get; private set; }
    public float LearningSpeedBonus { get; private set; }
    public float WorkshopMoneyBonus { get; private set; }
    public float ShopItemDiscount { get; private set; }
    public float ShipFireRateIncrease { get; private set; }
    public float ShipDamageIncrease { get; private set; }

    public List<InventoryItem> Inventory => _inventory;

    public void Initialize(List<InventoryItem> savedInventory)
    {
        _inventory = new List<InventoryItem>(savedInventory);
        RecalculateBonuses();
        OnInventoryUpdated?.Invoke(_inventory);
    }

    public void AddItem(Item item,int quantity = 1)
    {
        var inventoryItem = _inventory.Find(i => i.item.itemID == item.itemID);
        if (inventoryItem != null)
        {
            inventoryItem.quantity += quantity;
        }
        else
        {
            inventoryItem = new InventoryItem(item);
            inventoryItem.quantity = quantity;
            _inventory.Add(inventoryItem);
        }

        
        RecalculateBonuses();
        OnInventoryUpdated?.Invoke(_inventory);
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        RemoveItem(item.itemID, quantity);
    }

    public void RemoveItem(string itemId, int quantity = 1)
    {
        var inventoryItem = _inventory.Find(i => i.item.itemID == itemId);
        if (inventoryItem == null) return;

        inventoryItem.quantity -= quantity;

        if (inventoryItem.quantity <= 0)
        {
            _inventory.Remove(inventoryItem);
        }

        RecalculateBonuses();
        OnInventoryUpdated?.Invoke(_inventory);
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        RemoveItem(inventoryItem.item.itemID);
    }

    public int GetItemCount(Item item) => GetItemCount(item.itemID);

    public int GetItemCount(string itemId)
    {
        InventoryItem inventoryItem = _inventory.Find(i => i.item.itemID == itemId);
        return inventoryItem != null ? inventoryItem.quantity : 0;
    }

    private void ApplyItemBonuses(Item item)
    {
        if (!item.hasPassiveEffect) return;

        MoveSpeedBonus += item.overworldMoveSpeedBonus;
        MinigameMoveSpeedBonus += item.minigameMoveSpeedBonus;
        LearningSpeedBonus += item.learningSpeedBonus;
        WorkshopMoneyBonus += item.workMoneyBonus;
        ShopItemDiscount += item.shopItemDiscount;
        ShipFireRateIncrease += item.shipFireRateIncrease;
        ShipDamageIncrease += item.shipDamageIncrease;
    }

    private void RecalculateBonuses()
    {
        MoveSpeedBonus = 0;
        MinigameMoveSpeedBonus = 0;
        LearningSpeedBonus = 0;
        WorkshopMoneyBonus = 0;
        ShopItemDiscount = 0;
        ShipFireRateIncrease = 0;
        ShipDamageIncrease = 0;

        foreach (var inventoryItem in _inventory)
        {
            if (inventoryItem.item.hasPassiveEffect)
            {
                // Apply bonus per instance (e.g., if player has 3 of the same passive item)
                for (int i = 0; i < inventoryItem.quantity; i++)
                {
                    ApplyItemBonuses(inventoryItem.item);
                }
            }
        }
    }
}