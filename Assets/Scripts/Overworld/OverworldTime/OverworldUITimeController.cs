using TMPro;
using UnityEngine;

public class OverworldUITimeController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private GameObject _classTimeWarningPanel;

    private GameDataManager _gameDataManager;
    private OverworldTimeController _overworldTimeController;

    private void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnTimeUpdated += OnTimeUpdated;
        _gameDataManager.OnDayUpdated += OnDayUpdated;

        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _overworldTimeController.OnPastClassTime += OnPastClassTime;
    }
    private void OnTimeUpdated(string time)
    {
        _timeText.text = time;
    }

    private void OnDayUpdated(int day)
    {
        _dayText.text = $"Day {day}";
    }

    private void OnPastClassTime()
    {
        _classTimeWarningPanel.SetActive(true);
    }
}
