using System.Collections.Generic;
using UnityEngine;

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
public class GameData
{
    public string playerName = "Player";
    public float energy = 100;
    public float hunger = 100;
    public float stress = 0;

    public float maneuverability = 0;
    public float destruction = 0;
    public float mechanics = 0;

    public string currentTime = StaticValues.NEW_GAME_START_TIME;
    public int currentDay = 1;
    public int totalNumberOfDays = 10;

    public bool isTutorialEnabled = true;

    public List<DailyGameData> dailyGameDataList = new List<DailyGameData>();
}
