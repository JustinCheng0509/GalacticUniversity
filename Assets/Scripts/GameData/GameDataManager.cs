using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private GameData _gameData;

    public event Action OnGameDataLoaded;

    // Getters and Setters for GameData properties
    public GameData GameData => _gameData;
    
    public string PlayerName
    {
        get => _gameData.playerName;
        set => _gameData.playerName = value;
    }

    public float Energy
    {
        get => _gameData.energy;
        set => _gameData.energy = value;
    }

    public float Hunger
    {
        get => _gameData.hunger;
        set => _gameData.hunger = value;
    }

    public float Mood
    {
        get => _gameData.mood;
        set => _gameData.mood = value;
    }

    public float Money
    {
        get => _gameData.money;
        set => _gameData.money = value;
    }

    public float Maneuverability
    {
        get => _gameData.maneuverability;
        set => _gameData.maneuverability = value;
    }

    public float Destruction
    {
        get => _gameData.destruction;
        set => _gameData.destruction = value;
    }

    public float Mechanics
    {
        get => _gameData.mechanics;
        set => _gameData.mechanics = value;
    }

    public string CurrentTime
    {
        get => _gameData.currentTime;
        set => _gameData.currentTime = value;
    }

    public int CurrentDay
    {
        get => _gameData.currentDay;
        set => _gameData.currentDay = value;
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
    }

    public void RemoveQuest(Quest quest)
    {
        _gameData.activeQuests.Remove(quest);
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

    public bool IntroDialogPlayed
    {
        get => _gameData.introDialogPlayed;
        set => _gameData.introDialogPlayed = value;
    }

    public bool IntroClassPlayed
    {
        get => _gameData.introClassPlayed;
        set => _gameData.introClassPlayed = value;
    }

    public bool IntroShopPlayed
    {
        get => _gameData.introShopPlayed;
        set => _gameData.introShopPlayed = value;
    }

    public bool IntroDormPlayed
    {
        get => _gameData.introDormPlayed;
        set => _gameData.introDormPlayed = value;
    }

    public bool IntroWorkPlayed
    {
        get => _gameData.introWorkPlayed;
        set => _gameData.introWorkPlayed = value;
    }

    public bool IntroPlayRoomPlayed
    {
        get => _gameData.introPlayRoomPlayed;
        set => _gameData.introPlayRoomPlayed = value;
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
        OnGameDataLoaded?.Invoke();
    }
}
