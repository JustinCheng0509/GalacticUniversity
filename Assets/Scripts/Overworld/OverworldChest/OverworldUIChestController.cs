using UnityEngine;

public class OverworldUIChestController : MonoBehaviour
{
    [SerializeField] private GameObject _chestUI;
    [SerializeField] private GameObject _chestGrid;
    [SerializeField] private GameObject _itemPrefab;

    public void OpenChestUI(Chest chest)
    {
        // Clear the grid and add the items from the chest
        foreach (Transform child in _chestGrid.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in chest.items)
        {
            GameObject itemObject = Instantiate(_itemPrefab, _chestGrid.transform);
            itemObject.GetComponent<OverworldItemPrefabController>().SetItemInfo(item.itemSprite, () => {});
            // Set parent to the chest grid
            itemObject.transform.SetParent(_chestGrid.transform);
        }

        _chestUI.SetActive(true);
    }

    public void CloseChestUI()
    {
        _chestUI.SetActive(false);
    }
}
