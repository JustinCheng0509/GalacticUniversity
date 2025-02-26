using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OverworldUITimeController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dayText;
    [SerializeField] private GameObject _classTimeWarningPanel;

    private GameDataManager _gameDataManager;
    private OverworldTimeController _overworldTimeController;
    private TutorialController _tutorialController;

    private void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnTimeUpdated += OnTimeUpdated;
        _gameDataManager.OnDayUpdated += OnDayUpdated;

        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _overworldTimeController.OnPastClassTime += OnPastClassTime;
        _overworldTimeController.OnAfterClass += OnAfterClass;

        _tutorialController = FindAnyObjectByType<TutorialController>();
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

    private void OnAfterClass()
    {
        if (_gameDataManager.IsTutorialEnabled && _gameDataManager.Attendance == AttendanceStatus.ATTENDED) {
            _tutorialController.ShowTutorial(new List<string> { TutorialIDs.TUTORIAL_SHIP_CONTROL, TutorialIDs.TUTORIAL_LEADERBOARD });
        }
    }
}
