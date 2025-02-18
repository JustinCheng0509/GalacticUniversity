using TMPro;
using UnityEngine;

public class OverworldUIMoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void HandleMoneyUpdate(float money)
    {
        _moneyText.text = $"{Mathf.RoundToInt(money)}";
    }
}
