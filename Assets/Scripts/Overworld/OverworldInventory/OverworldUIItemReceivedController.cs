using UnityEngine;

public class OverworldUIItemReceivedController : MonoBehaviour
{
    [SerializeField] private GameObject _itemReceivedUI;
    [SerializeField] private GameObject _itemReceivedGrid;
    [SerializeField] private GameObject _itemPrefab;

    public void OpenChestUI(Chest chest)
    {
        // Clear the grid and add the items from the chest
        foreach (Transform child in _itemReceivedGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (ChestItemEntry chestItemEntry in chest.chestItems)
        {
            GameObject itemObject = Instantiate(_itemPrefab, _itemReceivedGrid.transform);
            if (chestItemEntry.item.isConsumable)
            {
                itemObject.GetComponent<OverworldItemPrefabController>().SetItemInfo(chestItemEntry.item.itemSprite, chestItemEntry.quantity, () => {});
            }
            else
            {
                itemObject.GetComponent<OverworldItemPrefabController>().SetItemInfo(chestItemEntry.item.itemSprite, () => {});
            }
            itemObject.transform.SetParent(_itemReceivedGrid.transform);
        }

        _itemReceivedUI.SetActive(true);
    }

    public void CloseItemReceivedUI()
    {
        _itemReceivedUI.SetActive(false);
    }
}
