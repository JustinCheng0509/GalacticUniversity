using System;
using TMPro;
using UnityEngine;

public class OverworldUINeedsController : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private TMP_Text _hungerText;
    [SerializeField] private TMP_Text _moodText;
    private OverworldPlayerStatusController _overworldPlayerStatusController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnNeedsUpdated += HandleNeedsUpdate;
    }

    void HandleNeedsUpdate (float energy, float hunger, float mood)
    {
        _energyText.text = Mathf.RoundToInt(energy).ToString();
        _hungerText.text = Mathf.RoundToInt(hunger).ToString();
        _moodText.text = Mathf.RoundToInt(mood).ToString();
    }
}
