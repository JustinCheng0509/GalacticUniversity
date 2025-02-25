using System;
using UnityEngine;

public class OverworldItemController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    public event Action<Item> OnSelectItem;

    public event Action<Item> OnUseItem;
    public event Action<Item> OnDiscardItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    public void SelectItem(Item item)
    {
        OnSelectItem?.Invoke(item);
    }

    public void UseItem(Item item)
    {
        if (!item.isConsumable)
        {
            return;
        }
        _gameDataManager.GameData.energy += item.energyRestore;
        _gameDataManager.GameData.hunger += item.hungerRestore;
        _gameDataManager.GameData.mood += item.moodRestore;
        // Remove the item from the inventory
        _gameDataManager.GameData.inventory.Remove(item);
        OnUseItem?.Invoke(item);
    }

    public void DiscardItem(Item item)
    {
        _gameDataManager.GameData.inventory.Remove(item);
        OnDiscardItem?.Invoke(item);
    }
}
