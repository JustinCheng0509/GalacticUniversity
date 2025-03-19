using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIInventoryController : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private GameObject _itemGrid;
    [SerializeField] private GameObject _itemSlotPrefab;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button _discardButton;

    private OverworldItemController _overworldItemController;
    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideInfo();
        _overworldItemController = FindAnyObjectByType<OverworldItemController>();
        _overworldItemController.OnSelectItem += OnSelectItem;

        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += ReloadItemList;
        _gameDataManager.OnInventoryUpdated += ReloadItemList;
    }

    private void ReloadItemList()
    {
        foreach (Transform child in _itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
        // Loop through the inventory and create a slot for each item
        foreach (Item item in _gameDataManager.Inventory)
        {
            GameObject itemSlot = Instantiate(_itemSlotPrefab, _itemGrid.transform);
            OverworldItemPrefabController itemSlotController = itemSlot.GetComponent<OverworldItemPrefabController>();
            itemSlotController.SetItemInfo(item.itemSprite, () => _overworldItemController.SelectItem(item));
            // Set the grid as the parent of the item slot
            itemSlot.transform.SetParent(_itemGrid.transform, false);
        }

        HideInfo();
    }

    private void HideInfo()
    {
        if (_gameDataManager != null && _gameDataManager.Inventory.Count == 0)
        {
            _itemNameText.text = "No items in inventory";
        } else
        {
            _itemNameText.text = "Select an item to view details";
        }
        _itemDescriptionText.text = "";
        _useButton.gameObject.SetActive(false);
        _discardButton.gameObject.SetActive(false);
    }

    private void ReloadItemList(List<Item> items)
    {
        ReloadItemList();
    }

    private void OnSelectItem(Item item)
    {
        _itemNameText.text = item.itemName;
        _itemDescriptionText.text = item.itemDescription;
        if (!item.isConsumable)
        {
            _useButton.gameObject.SetActive(false);
            _discardButton.gameObject.SetActive(false);
        } else
        {
            _useButton.gameObject.SetActive(true);
            _discardButton.gameObject.SetActive(true);
            _useButton.onClick.RemoveAllListeners();
            _useButton.onClick.AddListener(() => _overworldItemController.UseItem(item));
            _discardButton.onClick.RemoveAllListeners();
            _discardButton.onClick.AddListener(() => _overworldItemController.DiscardItem(item));
        }
    }
}
