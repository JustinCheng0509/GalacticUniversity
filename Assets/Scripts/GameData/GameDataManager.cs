using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private GameData _gameData;

    public event Action OnGameDataLoaded;

    // Getters and Setters for GameData properties
    public GameData GameData => _gameData;

    public event Action<AttendanceStatus> OnAttendanceUpdated;
    public event Action<float> OnHomeworkProgressUpdated;
    public event Action<float> OnEnergyUpdated;
    public event Action<float> OnHungerUpdated;
    public event Action<float> OnMoodUpdated;
    public event Action<float> OnManeuverabilityUpdated;
    public event Action<float> OnDestructionUpdated;
    public event Action<float> OnMechanicsUpdated;
    public event Action<float> OnMoneyUpdated;
    public event Action<string> OnTimeUpdated;
    public event Action<int> OnDayUpdated;
    public event Action<List<Quest>> OnActiveQuestsUpdated;
    
    public string PlayerName
    {
        get => _gameData.playerName;
        set => _gameData.playerName = value;
    }

    public float Energy
    {
        get => _gameData.energy;
        set {
            _gameData.energy = value;
            OnEnergyUpdated?.Invoke(_gameData.energy);
        }
        
    }

    public float Hunger
    {
        get => _gameData.hunger;
        set {
            _gameData.hunger = value;
            OnHungerUpdated?.Invoke(_gameData.hunger);
        }
    }

    public float Mood
    {
        get => _gameData.mood;
        set {
            _gameData.mood = value;
            OnMoodUpdated?.Invoke(_gameData.mood);
        }
    }

    public float Money
    {
        get => _gameData.money;
        set {
            _gameData.money = value;
            OnMoneyUpdated?.Invoke(_gameData.money);
        }
    }

    public float Maneuverability
    {
        get => _gameData.maneuverability;
        set {
            _gameData.maneuverability = value;
            OnManeuverabilityUpdated?.Invoke(_gameData.maneuverability);
        }
    }

    public float Destruction
    {
        get => _gameData.destruction;
        set {
            _gameData.destruction = value;
            OnDestructionUpdated?.Invoke(_gameData.destruction);
        }
    }

    public float Mechanics
    {
        get => _gameData.mechanics;
        set {
            _gameData.mechanics = value;
            OnMechanicsUpdated?.Invoke(_gameData.mechanics);
        }
    }

    public string CurrentTime
    {
        get => _gameData.currentTime;
        set {
            _gameData.currentTime = value;
            OnTimeUpdated?.Invoke(_gameData.currentTime);
        }
    }

    public int CurrentDay
    {
        get => _gameData.currentDay;
        set {
            _gameData.currentDay = value;
            OnDayUpdated?.Invoke(_gameData.currentDay);
            OnAttendanceUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].attendance);
            OnHomeworkProgressUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress);
        }
    }

    public int TotalNumberOfDays
    {
        get => _gameData.totalNumberOfDays;
        set => _gameData.totalNumberOfDays = value;
    }

    public bool IsTutorialEnabled
    {
        get => _gameData.isTutorialEnabled;
        set => _gameData.isTutorialEnabled = value;
    }

    public bool IntroDialogPlayed
    {
        get => _gameData.introDialogPlayed;
        set => _gameData.introDialogPlayed = value;
    }

    public int TotalScore
    {
        get => _gameData.totalScore;
        set => _gameData.totalScore = value;
    }

    public int TotalDestructionScore
    {
        get => _gameData.totalDestructionScore;
        set => _gameData.totalDestructionScore = value;
    }

    public int TotalSafetyScore
    {
        get => _gameData.totalSafetyScore;
        set => _gameData.totalSafetyScore = value;
    }

    public List<Quest> ActiveQuests
    {
        get => _gameData.activeQuests;
        set => _gameData.activeQuests = value;
    }

    public void AddQuest(Quest quest)
    {
        // if quest is already in the list, don't add it again
        if (_gameData.activeQuests.Contains(quest))
        {
            return;
        }
        _gameData.activeQuests.Add(quest);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }

    public void RemoveQuest(Quest quest)
    {
        _gameData.activeQuests.Remove(quest);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }

    public List<DailyGameData> DailyGameDataList
    {
        get => _gameData.dailyGameDataList;
        set => _gameData.dailyGameDataList = value;
    }

    public List<LeaderboardEntry> Leaderboard
    {
        get => _gameData.leaderboard;
        set => _gameData.leaderboard = value;
    }

    public AttendanceStatus Attendance
    {
        get => _gameData.dailyGameDataList[_gameData.currentDay - 1].attendance;
        set => _gameData.dailyGameDataList[_gameData.currentDay - 1].attendance = value;
    }

    public float HomeworkProgress
    {
        get => _gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress;
        set => _gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress = value;
    }

    public bool IsTutorialCompleted(string tutorialId)
    {
        return _gameData.tutorialsCompleted.Contains(tutorialId);
    }

    public void CompleteTutorial(string tutorialId)
    {
        if (!_gameData.tutorialsCompleted.Contains(tutorialId))
        {
            _gameData.tutorialsCompleted.Add(tutorialId);
        }
    }

    void Start()
    {
        _gameData = SavedDataManager.LoadGameData();
        OnAttendanceUpdated?.Invoke(Attendance);
        OnHomeworkProgressUpdated?.Invoke(HomeworkProgress);
        OnEnergyUpdated?.Invoke(Energy);
        OnHungerUpdated?.Invoke(Hunger);
        OnMoodUpdated?.Invoke(Mood);
        OnManeuverabilityUpdated?.Invoke(Maneuverability);
        OnDestructionUpdated?.Invoke(Destruction);
        OnMechanicsUpdated?.Invoke(Mechanics);
        OnMoneyUpdated?.Invoke(Money);
        OnTimeUpdated?.Invoke(CurrentTime);
        OnDayUpdated?.Invoke(CurrentDay);
        OnActiveQuestsUpdated?.Invoke(ActiveQuests);
        OnGameDataLoaded?.Invoke();
    }
}
