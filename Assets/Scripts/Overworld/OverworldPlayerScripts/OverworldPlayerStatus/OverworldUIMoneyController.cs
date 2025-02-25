using TMPro;
using UnityEngine;

public class OverworldUIMoneyController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    [SerializeField] private TMP_Text _moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnMoneyUpdated += HandleMoneyUpdate;
    }

    private void HandleMoneyUpdate(float money)
    {
        _moneyText.text = $"{Mathf.RoundToInt(money)}";
    }
}
