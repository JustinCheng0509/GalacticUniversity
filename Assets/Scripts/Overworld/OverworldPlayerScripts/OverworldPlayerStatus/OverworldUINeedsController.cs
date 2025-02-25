using System;
using TMPro;
using UnityEngine;

public class OverworldUINeedsController : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private TMP_Text _hungerText;
    [SerializeField] private TMP_Text _moodText;
    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnEnergyUpdated += HandleEnergyUpdate;
        _gameDataManager.OnHungerUpdated += HandleHungerUpdate;
        _gameDataManager.OnMoodUpdated += HandleMoodUpdate;
    }

    void HandleEnergyUpdate (float energy)
    {
        _energyText.text = Mathf.RoundToInt(energy).ToString();
    }

    void HandleHungerUpdate (float hunger)
    {
        _hungerText.text = Mathf.RoundToInt(hunger).ToString();
    }

    void HandleMoodUpdate (float mood)
    {
        _moodText.text = Mathf.RoundToInt(mood).ToString();
    }
}
