using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverworldPlayerStatus
{
    Idle,
    Walking,
    Running,
    Sleeping,
    Chatting,
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

    public bool IsBusy => _currentStatus == OverworldPlayerStatus.Sleeping || _currentStatus == OverworldPlayerStatus.DoingHomework || _currentStatus == OverworldPlayerStatus.Working || _currentStatus == OverworldPlayerStatus.Playing || _currentStatus == OverworldPlayerStatus.Chatting;

    private GameDataManager _gameDataManager;
    private OverworldTimeController _overworldTimeController;
    private OverworldNPCInteractionController _overworldNPCInteractionController;
    private TutorialController _tutorialController;

    private Coroutine _updateStatusCoroutine;
    public event Action<OverworldPlayerStatus> OnStatusChanged;

    private void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;

        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _overworldTimeController.OnPastClassLateTime += HandleLateClassTime;

        _overworldNPCInteractionController = FindAnyObjectByType<OverworldNPCInteractionController>();
        _overworldNPCInteractionController.OnNPCStartChat += () => CurrentStatus = OverworldPlayerStatus.Chatting;

        _tutorialController = FindAnyObjectByType<TutorialController>();
    }

    private void HandleLateClassTime()
    {
        if (_gameDataManager.Attendance == AttendanceStatus.NOT_STARTED)
        {
            _gameDataManager.Attendance = AttendanceStatus.ABSENT;
            _gameDataManager.GenerateLeaderboardScore();
            _tutorialController.ShowTutorial(new List<string> { TutorialIDs.TUTORIAL_ABSENT, TutorialIDs.TUTORIAL_LEADERBOARD});
        }
    }

    private void OnGameDataLoaded()
    {
        UpdateStatus();
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
            UpdateNPCRelationships();
            CheckForLowStats();

            yield return new WaitForSeconds(_overworldTimeController.IntervalBetweenMinute);
        }
    }

    private void UpdateNPCRelationships()
    {
        if (_currentStatus == OverworldPlayerStatus.Chatting)
        {
            _gameDataManager.UpdateNPCRelationship(_overworldNPCInteractionController.CurrentNPC.npcID, 0.5f);
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
        } else if (_currentStatus == OverworldPlayerStatus.Chatting) {
            _gameDataManager.Mood += 0.2f;
        }
    }

    private void UpdateHomework()
    {
        if (_currentStatus == OverworldPlayerStatus.DoingHomework && _gameDataManager.HomeworkProgress < 100)
        {
            _gameDataManager.HomeworkProgress += 1f;

            int chanceToIncreaseSkill = 5 + (int) _gameDataManager.LeaningSpeedBonus;
            TryIncreaseRandomSkill(chanceToIncreaseSkill);
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
            float moneyGained = 1f + 1f * _gameDataManager.WorkshopMoneyBonus / 100f;
            _gameDataManager.Money += moneyGained;
            _gameDataManager.TotalWorkshopMinutes += 1;
            
            int chanceToIncreaseSkill = 5 + (int) _gameDataManager.LeaningSpeedBonus;
            TryIncreaseRandomSkill(chanceToIncreaseSkill);
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
