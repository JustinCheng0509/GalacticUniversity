using System;
using System.Collections;
using UnityEngine;

public class OverworldTimeController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private TutorialController _tutorialController;
    private IEnumerator _timeCoroutine;

    private const float BASE_INTERVAL_BETWEEN_MINUTES = 0.1f;

    public float IntervalBetweenMinute => CanAttendClass ? BASE_INTERVAL_BETWEEN_MINUTES * 15 : BASE_INTERVAL_BETWEEN_MINUTES;
    public bool CanAttendClass => IsWithinTimeRange(_gameDataManager.CurrentTime, GameConstants.CLASS_START_TIME, GameConstants.CLASS_LATE_TIME);
    public bool IsPastLateTime => IsAfterTime(_gameDataManager.CurrentTime, GameConstants.CLASS_LATE_TIME);
    public bool IsAfterClass => IsAfterTime(_gameDataManager.CurrentTime, GameConstants.CLASS_END_TIME);

    public event Action OnNewDayStarted;
    public event Action OnTimeAdvanced;
    public event Action OnPastClassTime;
    public event Action OnPastClassLateTime;
    public event Action OnAfterClass;
    public event Action OnLastDayEnded;

    private void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;
        _timeCoroutine = TimeCoroutine();

        _tutorialController = FindAnyObjectByType<TutorialController>();
    }

    private void OnGameDataLoaded()
    {
        StartCoroutine(_timeCoroutine);
    }

    public static float GetTimePercentage(string currentTime)
    {
        if (!TryParseTime(currentTime, out int hour, out int minute)) return 0f;
        return (hour * 60 + minute) / 1440f;
    }

    private IEnumerator TimeCoroutine()
    {
        while (true)
        {
            AdvanceTime();
            if (_gameDataManager.IsTutorialEnabled && CanAttendClass) {
                _tutorialController.ShowTutorial(TutorialIDs.TUTORIAL_CLASS_TIME);
            }
            yield return new WaitForSeconds(IntervalBetweenMinute);
        }
    }

    private void AdvanceTime()
    {
        if (!TryParseTime(_gameDataManager.CurrentTime, out int hour, out int minute)) return;

        minute++;
        if (minute == 60)
        {
            minute = 0;
            hour++;
            if (hour == 24)
            {  
                if (_gameDataManager.CurrentDay == GameConstants.TOTAL_NUMBER_OF_DAYS)
                {   
                    _gameDataManager.CurrentTime = "23:50";
                    SavedDataManager.SaveGameData(_gameDataManager.GameData);
                    OnLastDayEnded?.Invoke();
                }
                else
                {
                    hour = 0;
                    _gameDataManager.CurrentDay++;
                    SavedDataManager.SaveGameData(_gameDataManager.GameData);
                    OnNewDayStarted?.Invoke();
                }
            }
        }
        
        _gameDataManager.CurrentTime = $"{hour:00}:{minute:00}";

        OnTimeAdvanced?.Invoke();
        if (IsPastLateTime) OnPastClassLateTime?.Invoke();
        if (IsAfterClass) OnAfterClass?.Invoke();
        if (CanAttendClass) OnPastClassTime?.Invoke();
    }

    public static bool IsWithinTimeRange(string currentTime, string startTime, string endTime)
    {
        return CompareTimes(currentTime, startTime) >= 0 && CompareTimes(currentTime, endTime) <= 0;
    }

    public static bool IsAfterTime(string currentTime, string compareTime)
    {
        return CompareTimes(currentTime, compareTime) > 0;
    }

    private static int CompareTimes(string time1, string time2)
    {
        if (!TryParseTime(time1, out int hour1, out int minute1) || !TryParseTime(time2, out int hour2, out int minute2))
        {
            return 0;
        }
        return (hour1 * 60 + minute1).CompareTo(hour2 * 60 + minute2);
    }

    private static bool TryParseTime(string time, out int hour, out int minute)
    {
        string[] parts = time.Split(':');
        hour = 0;
        minute = 0;
        return parts.Length == 2 && int.TryParse(parts[0], out hour) && int.TryParse(parts[1], out minute);
    }
}