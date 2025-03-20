using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverworldUIShopController : MonoBehaviour
{
    [SerializeField] private List<Item> _shopItems;
    [SerializeField] private TMP_Text _itemNameText;
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private TMP_Text _itemValueText;
    [SerializeField] private TMP_Text _notEnoughMoneyText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _itemGrid;
    [SerializeField] private GameObject _itemSlotPrefab;
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioClip _buyItemAudioClip;
    [SerializeField] private GameObject ShopUI;
    private GameDataManager _gameDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        LoadItemList();
    }

    private void LoadItemList()
    {
        foreach (Transform child in _itemGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Item item in _shopItems)
        {
            GameObject itemSlot = Instantiate(_itemSlotPrefab, _itemGrid.transform);
            OverworldItemPrefabController itemSlotController = itemSlot.GetComponent<OverworldItemPrefabController>();
            itemSlotController.SetItemInfo(item.itemSprite, () => SelectItem(item));
            // Set the grid as the parent of the item slot
            itemSlot.transform.SetParent(_itemGrid.transform, false);
        }
        HideInfo();
    }

    public void OpenShopUI()
    {
        ShopUI.SetActive(true);
    }

    private void HideInfo()
    {
        _itemNameText.text = "Select an item to view details";
        _itemDescriptionText.text = "";
        _itemValueText.text = "";
        _buyButton.onClick.RemoveAllListeners();
        _buyButton.gameObject.SetActive(false);
        _notEnoughMoneyText.gameObject.SetActive(false);
    }

    public void SelectItem(Item item)
    {
        _notEnoughMoneyText.gameObject.SetActive(false);
        _itemNameText.text = item.itemName;
        _itemDescriptionText.text = OverworldUIInventoryController.AppendItemDescription(item);
        _itemValueText.text = "$" + item.itemValue.ToString();
        _buyButton.onClick.AddListener(() => BuyItem(item));
        _buyButton.gameObject.SetActive(true);
    }

    public void BuyItem(Item item)
    {
        if (_gameDataManager.Money < item.itemValue)
        {
            _notEnoughMoneyText.gameObject.SetActive(true);
            return;
        }
        // Add the item to the inventory and subtract the value from the player's money
        _gameDataManager.AddItemToInventory(item);
        _gameDataManager.Money -= item.itemValue;
        _sfxAudioSource.PlayOneShot(_buyItemAudioClip);
        HideInfo();
    }
}
