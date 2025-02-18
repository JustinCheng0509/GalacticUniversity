using System;
using System.Collections;
using UnityEngine;

public enum OverworldPlayerStatus
{
    Idle,
    Walking,
    Running,
    Sleeping,
    DoingHomework,
    Working,
    Playing
}

public class OverworldPlayerStatusController : MonoBehaviour
{
    private OverworldPlayerStatus _currentStatus = OverworldPlayerStatus.Idle;

    public OverworldPlayerStatus CurrentStatus
{
        get => _currentStatus;
        set
        {
            if (_currentStatus != value)
            {
                _currentStatus = value;
                OnStatusChanged?.Invoke(_currentStatus);
            }
        }
    }

    public bool IsBusy => _currentStatus == OverworldPlayerStatus.Sleeping || _currentStatus == OverworldPlayerStatus.DoingHomework || _currentStatus == OverworldPlayerStatus.Working || _currentStatus == OverworldPlayerStatus.Playing;

    private GameDataManager _gameDataManager;
    private OverworldTimeController _overworldTimeController;

    private Coroutine _updateStatusCoroutine;
    public event Action<OverworldPlayerStatus> OnStatusChanged;

    public event Action<AttendanceStatus> OnAttendanceUpdated;
    public event Action<float> OnHomeworkProgressUpdated;
    public event Action<float, float, float> OnNeedsUpdated;

    private void Start()
    {
        _gameDataManager = FindFirstObjectByType<GameDataManager>();
        _overworldTimeController = FindFirstObjectByType<OverworldTimeController>();

        _overworldTimeController.OnPastClassLateTime += HandleLateClassTime;
        _overworldTimeController.OnNewDayStarted += HandleNewDayStarted;
    }

    private void HandleNewDayStarted()
    {
        OnAttendanceUpdated?.Invoke(_gameDataManager.Attendance);
        OnHomeworkProgressUpdated?.Invoke(_gameDataManager.HomeworkProgress);
    }

    private void HandleLateClassTime()
    {
        if (_gameDataManager.Attendance != AttendanceStatus.ATTENDED)
        {
            _gameDataManager.Attendance = AttendanceStatus.ABSENT;
            OnAttendanceUpdated?.Invoke(_gameDataManager.Attendance);
        }
    }

    public void UpdateStatus()
    {
        if (_updateStatusCoroutine != null)
        {
            StopCoroutine(_updateStatusCoroutine);
        }
        _updateStatusCoroutine = StartCoroutine(UpdateStatusCoroutine());
    }

    private IEnumerator UpdateStatusCoroutine()
    {
        while (true)
        {
            UpdateEnergy();
            UpdateHunger();
            UpdateMood();
            UpdateHomework();
            UpdateWork();

            ClampStats();
            OnNeedsUpdated?.Invoke(_gameDataManager.Energy, _gameDataManager.Hunger, _gameDataManager.Mood);

            CheckForLowStats();

            yield return new WaitForSeconds(_overworldTimeController.IntervalBetweenMinute);
        }
    }

    private void UpdateEnergy()
    {
        _gameDataManager.Energy += _currentStatus == OverworldPlayerStatus.Sleeping ? 0.18f : -0.06f;
    }

    private void UpdateHunger()
    {
        _gameDataManager.Hunger -= 0.06f;
    }

    private void UpdateMood()
    {
        if (_currentStatus == OverworldPlayerStatus.DoingHomework || _currentStatus == OverworldPlayerStatus.Working)
        {
            _gameDataManager.Mood -= 0.2f;
        } else if (_currentStatus == OverworldPlayerStatus.Playing)
        {
            _gameDataManager.Mood += 0.4f;
        }
    }

    private void UpdateHomework()
    {
        if (_currentStatus == OverworldPlayerStatus.DoingHomework && _gameDataManager.HomeworkProgress < 100)
        {
            _gameDataManager.HomeworkProgress += 1f;
            OnHomeworkProgressUpdated?.Invoke(_gameDataManager.HomeworkProgress);

            if (GameConstants.USE_SKILL_SYSTEM)
            {
                TryIncreaseRandomSkill(5);
            }
        }

        if (_gameDataManager.HomeworkProgress >= 100)
        {
            CancelHomeworkAction();
        }
    }

    private void UpdateWork()
    {
        if (_currentStatus == OverworldPlayerStatus.Working)
        {
            _gameDataManager.Money += 1;
            if (GameConstants.USE_SKILL_SYSTEM)
            {
                TryIncreaseRandomSkill(5);
            }
        }
    }

    private void TryIncreaseRandomSkill(int chancePercent)
    {
        if (UnityEngine.Random.Range(0, 100) < chancePercent)
        {
            int skillToIncrease = UnityEngine.Random.Range(0, 3);
            switch (skillToIncrease)
            {
                case 0:
                    _gameDataManager.Maneuverability += 1;
                    break;
                case 1:
                    _gameDataManager.Destruction += 1;
                    break;
                case 2:
                    _gameDataManager.Mechanics += 1;
                    break;
            }
        }
    }

    private void ClampStats()
    {
        _gameDataManager.Energy = Mathf.Clamp(_gameDataManager.Energy, 0, 100);
        _gameDataManager.Hunger = Mathf.Clamp(_gameDataManager.Hunger, 0, 100);
        _gameDataManager.Mood = Mathf.Clamp(_gameDataManager.Mood, 0, 100);
    }

    private void CheckForLowStats()
    {
        if (_gameDataManager.Hunger < 20 || _gameDataManager.Energy < 20 || _gameDataManager.Mood < 20)
        {
            if (_currentStatus == OverworldPlayerStatus.DoingHomework)
            {
                CancelHomeworkAction();
            }
            else if (_currentStatus == OverworldPlayerStatus.Working)
            {
                CancelWorkAction();
            }
        }
    }

    private void CancelHomeworkAction()
    {
        if (_currentStatus == OverworldPlayerStatus.DoingHomework)
        {
            CurrentStatus = OverworldPlayerStatus.Idle;
        }
    }

    private void CancelWorkAction()
    {
        if (_currentStatus == OverworldPlayerStatus.Working)
        {
            CurrentStatus = OverworldPlayerStatus.Idle;
        }
    }
}
