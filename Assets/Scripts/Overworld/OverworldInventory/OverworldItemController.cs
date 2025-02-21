using System;
using UnityEngine;

public class OverworldItemController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    private Item _selectedItem;

    public event Action<Item> OnSelectItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;
    }

    public void SelectItem(Item item)
    {
        _selectedItem = item;
        OnSelectItem?.Invoke(item);
    }

    // Update is called once per frame
    private void OnGameDataLoaded()
    {
        // Load the item data
        // LoadItemData();
    }


}
