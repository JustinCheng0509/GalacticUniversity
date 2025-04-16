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
        _gameDataManager.InventoryManager.OnInventoryUpdated += ReloadItemList;
    }

    private void ReloadItemList()
    {
        foreach (Transform child in _itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
        // Loop through the inventory and create a slot for each item
        foreach (InventoryItem item in _gameDataManager.InventoryManager.Inventory)
        {
            GameObject itemSlot = Instantiate(_itemSlotPrefab, _itemGrid.transform);
            OverworldItemPrefabController itemSlotController = itemSlot.GetComponent<OverworldItemPrefabController>();
            if (item.item.isConsumable)
            {
                itemSlotController.SetItemInfo(item.item.itemSprite, item.quantity, () => _overworldItemController.SelectItem(item.item));
            } else
            {
                itemSlotController.SetItemInfo(item.item.itemSprite, () => _overworldItemController.SelectItem(item.item));
            }
            // Set the grid as the parent of the item slot
            itemSlot.transform.SetParent(_itemGrid.transform, false);
        }

        HideInfo();
    }

    private void HideInfo()
    {
        if (_gameDataManager != null && _gameDataManager.InventoryManager.Inventory.Count == 0)
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

    private void ReloadItemList(List<InventoryItem> items)
    {
        ReloadItemList();
    }

    private void OnSelectItem(Item item)
    {
        _itemNameText.text = item.itemName;
        _itemDescriptionText.text = AppendItemDescription(item);
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

    public static string AppendItemDescription(Item item)
    {
        string description = item.itemDescription;
        description += "\n\n";
        if (item.energyRestore > 0)
        {
            description += "Energy Restore: +" + item.energyRestore + "\n";
        }
        if (item.hungerRestore > 0)
        {
            description += "Hunger restore: +" + item.hungerRestore + "\n";
        }
        if (item.moodRestore > 0)
        {
            description += "Mood restore: +" + item.moodRestore + "\n";
        }
        if (item.overworldMoveSpeedBonus > 0)
        {
            description += "Campus move speed bonus: +" + item.overworldMoveSpeedBonus + "%\n";
        }
        if (item.minigameMoveSpeedBonus > 0)
        {
            description += "Minigame move speed bonus: +" + item.minigameMoveSpeedBonus + "%\n";
        }
        if (item.learningSpeedBonus > 0)
        {
            description += "Chance to gain skills: +" + item.learningSpeedBonus + "%\n";
        }
        if (item.shopItemDiscount > 0)
        {
            description += "Shop discount: -" + item.shopItemDiscount + "%\n";
        }
        if (item.workMoneyBonus > 0)
        {
            description += "Bonus money from workshop: +" + item.workMoneyBonus + "%\n";
        }
        if (item.shipFireRateIncrease > 0)
        {
            description += "Ship fire rate increase: +" + item.shipFireRateIncrease + "%\n";
        }
        if (item.shipDamageIncrease > 0)
        {
            description += "Ship damage increase: +" + item.shipDamageIncrease + "%\n";
        }
        return description;
    }
}
