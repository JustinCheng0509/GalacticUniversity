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

    public string currentTime = StaticValues.NEW_GAME_START_TIME;
    public int currentDay = 1;
    public int totalNumberOfDays = 10;

    public bool isTutorialEnabled = true;

    public int totalScore = 0;
    public int totalDestructionScore = 0;
    public int totalSafetyScore = 0;

    public List<DailyGameData> dailyGameDataList = new List<DailyGameData>();

    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
}
