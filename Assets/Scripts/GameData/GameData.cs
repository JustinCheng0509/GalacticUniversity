using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AttendanceStatus
{
    NOT_STARTED,
    ATTENDED,
    ABSENT
}

[System.Serializable]
public class DailyGameData
{
    public float homeworkProgress = 0;
    public AttendanceStatus attendance = AttendanceStatus.NOT_STARTED;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string name = "name";
    public int totalScore = 0;
    public int destructionScore = 0;
    public int safetyScore = 0;
}


[System.Serializable]
public class GameData
{
    public string playerName = "Player";
    public float energy = 100;
    public float hunger = 100;
    public float mood = 100;
    public float money = 200;

    public float maneuverability = 0;
    public float destruction = 0;
    public float mechanics = 0;

    public string currentTime = GameConstants.NEW_GAME_START_TIME;
    public int currentDay = 1;
    public int totalNumberOfDays = 10;

    public int totalScore = 0;
    public int totalDestructionScore = 0;
    public int totalSafetyScore = 0;

    public List<Quest> activeQuests = new List<Quest>();
    public List<string> tutorialsCompleted = new List<string>();

    public List<Item> inventory = new List<Item>();

    public List<DailyGameData> dailyGameDataList = new List<DailyGameData>();

    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    // Intros and tutorials
    public bool isTutorialEnabled = true;
    public bool introDialogPlayed = false;
    public bool introClassPlayed = false;
    public bool introShopPlayed = false;
    public bool introDormPlayed = false;
    public bool introPlayRoomPlayed = false;
    public bool introWorkPlayed = false;
}
