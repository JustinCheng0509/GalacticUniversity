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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldItemController = FindAnyObjectByType<OverworldItemController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
