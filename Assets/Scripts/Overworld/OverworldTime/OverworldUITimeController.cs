using TMPro;
using UnityEngine;

public class OverworldUITimeController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dayText;

    private GameDataManager _gameDataManager;

    private void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnTimeUpdated += OnTimeUpdated;
        _gameDataManager.OnDayUpdated += OnDayUpdated;
    }
    private void OnTimeUpdated(string time)
    {
        _timeText.text = time;
    }

    private void OnDayUpdated(int day)
    {
        _dayText.text = $"Day {day}";
    }
}
