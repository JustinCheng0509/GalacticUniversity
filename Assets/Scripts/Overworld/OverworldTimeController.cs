using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class OverworldTimeController : MonoBehaviour
{
    private string _currentTime = GameConstants.NEW_GAME_START_TIME; // 08:00

    public string CurrentTime
    {
        get { return _currentTime; }
        set { _currentTime = value; }
    }

    // 1 means a minute in the game is 1 second in real life
    private const float BASE_INTERVAL_BETWEEN_MINUTES = 0.2f;

    public float IntervalBetweenMinute => CanAttendClass ? BASE_INTERVAL_BETWEEN_MINUTES * 15 : BASE_INTERVAL_BETWEEN_MINUTES;
    public bool CanAttendClass => IsWithinTimeRange(GameConstants.CLASS_START_TIME, GameConstants.CLASS_LATE_TIME);
    public bool IsPastLateTime => IsAfterTime(GameConstants.CLASS_LATE_TIME);
    public bool IsAfterClass => IsAfterTime(GameConstants.CLASS_END_TIME);

    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _dayText;

    private IEnumerator _timeCoroutine;

    private GameDataManager _gameDataManager;

    public event Action OnNewDayStarted;
    public event Action OnTimeAdvanced;
    public event Action OnPastClassTime;
    public event Action OnPastClassLateTime;
    public event Action OnAfterClass;
    public event Action OnLastDayEnded;

    private bool IsWithinTimeRange(string startTime, string endTime)
    {
        if (!TryParseTime(CurrentTime, out int hour, out int minute) ||
            !TryParseTime(startTime, out int startHour, out int startMinute) ||
            !TryParseTime(endTime, out int endHour, out int endMinute))
        {
            return false;
        }

        return (hour > startHour || (hour == startHour && minute >= startMinute)) &&
               (hour < endHour || (hour == endHour && minute <= endMinute));
    }

    private bool IsAfterTime(string compareTime)
    {
        if (!TryParseTime(CurrentTime, out int hour, out int minute) ||
            !TryParseTime(compareTime, out int compareHour, out int compareMinute))
        {
            return false;
        }

        return hour > compareHour || (hour == compareHour && minute > compareMinute);
    }

    private static bool TryParseTime(string time, out int hour, out int minute)
    {
        string[] parts = time.Split(':');
        hour = 0;
        minute = 0;
        return parts.Length == 2 && int.TryParse(parts[0], out hour) && int.TryParse(parts[1], out minute);
    }

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;

        _timeText.text = CurrentTime;
        _timeCoroutine = TimeCoroutine();
    }

    private void OnGameDataLoaded()
    {
        CurrentTime = _gameDataManager.CurrentTime;
        _timeText.text = CurrentTime;
        _dayText.text = $"Day {_gameDataManager.CurrentDay}";
        StartCoroutine(_timeCoroutine);
    }

    public float GetTimePercentage()
    {
        string[] time = _currentTime.Split(':');
        int hour = int.Parse(time[0]);
        int minute = int.Parse(time[1]);

        return (hour * 60 + minute) / 1440f;
    }

    IEnumerator TimeCoroutine()
    {
        while (true)
        {
            UpdateTime();
            if (CanAttendClass)
            {
                // if (PlayerPrefs.GetInt("tutorialAttendClass") == 0)
                // {
                //     PlayerPrefs.SetInt("tutorialAttendClass", 1);
                //     tutorialController.ShowTutorial(tutorialController.classTimeTutorial);
                // }
            }
            yield return new WaitForSeconds(IntervalBetweenMinute);
        }
    }

    void UpdateTime()
    {

        // if (isAfterClass && playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED)
        // {
        //     if (GameConstants.USE_SKILL_SYSTEM && PlayerPrefs.GetInt("ShipControlTutorial", 0) == 0)
        //     {
        //         PlayerPrefs.SetInt("ShipControlTutorial", 1);
        //         tutorialController.ShowTutorial(tutorialController.shipControlTutorial);
        //     } else if (!GameConstants.USE_SKILL_SYSTEM && PlayerPrefs.GetInt("LeaderboardTutorial", 0) == 0)
        //     {
        //         PlayerPrefs.SetInt("LeaderboardTutorial", 1);
        //         tutorialController.ShowTutorial(tutorialController.leaderboardTutorial);
        //     }
        // }

        AdvanceTime();

        _timeText.text = _currentTime;
        _gameDataManager.CurrentTime = _currentTime;
    }

    private void AdvanceTime()
    {
        if (!TryParseTime(_currentTime, out int hour, out int minute)) return;

        minute++;
        if (minute == 60)
        {
            minute = 0;
            hour++;
            if (hour == 24)
            {
                hour = 0;
                if (_gameDataManager.CurrentDay == GameConstants.TOTAL_NUMBER_OF_DAYS)
                {
                    OnLastDayEnded?.Invoke();
                } else {
                    _gameDataManager.CurrentDay++;
                    SavedDataManager.SaveGameData(_gameDataManager.GameData);
                    OnNewDayStarted?.Invoke();
                }
                _dayText.text = $"Day {_gameDataManager.CurrentDay}";
                // if (playerInfo.gameData.currentDay == 5)
                // {
                //     Time.timeScale = 0;
                //     overworldSwitchScene.gameEndPanel.SetActive(true);
                // }
                // playerInfo.gameData.currentDay++;
                // SavedDataManager.SaveGameData(playerInfo.gameData);
            }
        }
        _currentTime = $"{hour:00}:{minute:00}";

        // Trigger the general time event
        OnTimeAdvanced?.Invoke();

        // Trigger specific time-based events
        if (IsPastLateTime) OnPastClassLateTime?.Invoke();
        if (IsAfterClass) OnAfterClass?.Invoke();
        if (CanAttendClass) OnPastClassTime?.Invoke();
    }
}
