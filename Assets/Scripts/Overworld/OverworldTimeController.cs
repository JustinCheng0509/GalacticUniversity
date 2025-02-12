using System.Collections;
using TMPro;
using UnityEngine;

public class OverworldTimeController : MonoBehaviour
{
    public string currentTime = "07:00";

    // 1 means a minute in the game is 1 second in real life
    public float intervalBetweenMinute = 0.01f;

    [SerializeField]
    private TMP_Text timeText;

    IEnumerator timeCoroutine;

    [SerializeField] private OverworldSwitchScene overworldSwitchScene;

    [SerializeField] private PlayerInfo playerInfo;

    [SerializeField] private GameDataManager gameDataManager;

    [SerializeField] private TutorialController tutorialController;

    public bool canAttendClass
    {
        get
        {
            // between 14:00 and 14:30
            string[] time = currentTime.Split(':');
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            if (hour == 14 && minute >= 0 && minute <= 30)
            {
                return true;
            }
            return false;
        }
    }

    public bool isAbsent
    {
        get
        {
            // after 14:30 and status is not attended
            string[] time = currentTime.Split(':');
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            if (hour == 14 && minute > 30)
            {
                if (playerInfo.GetAttendanceStatus() != AttendanceStatus.ATTENDED)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public bool isAfterClass
    {
        get
        {
            // after 16:00
            string[] time = currentTime.Split(':');
            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);
            if (hour == 16 && minute > 0)
            {
                return true;
            }
            return false;
        }
    }

    void Start()
    {
        currentTime = playerInfo.gameData.currentTime;
        timeText.text = currentTime;
        timeCoroutine = TimeCoroutine(intervalBetweenMinute);
        StartCoroutine(timeCoroutine);
    }

    public float GetTimePercentage()
    {
        string[] time = currentTime.Split(':');
        int hour = int.Parse(time[0]);
        int minute = int.Parse(time[1]);

        return (hour * 60 + minute) / 1440f;
    }

    IEnumerator TimeCoroutine(float interval)
    {
        while (true)
        {
            if (canAttendClass)
            {
                if (PlayerPrefs.GetInt("tutorialAttendClass") == 0)
                {
                    PlayerPrefs.SetInt("tutorialAttendClass", 1);
                    tutorialController.ShowTutorial(tutorialController.classTimeTutorial);
                }
                // slow down the interval
                yield return new WaitForSeconds(interval * 15);
            } else {
                yield return new WaitForSeconds(interval);
            }
            UpdateTime();
        }
    }

    void UpdateTime()
    {
        if (isAbsent)
        {
            playerInfo.SetAttendanceStatus(AttendanceStatus.ABSENT);
        }

        if (isAfterClass && playerInfo.GetAttendanceStatus() == AttendanceStatus.ATTENDED)
        {
            if (StaticValues.USE_SKILL_SYSTEM && PlayerPrefs.GetInt("ShipControlTutorial", 0) == 0)
            {
                PlayerPrefs.SetInt("ShipControlTutorial", 1);
                tutorialController.ShowTutorial(tutorialController.shipControlTutorial);
            } else if (!StaticValues.USE_SKILL_SYSTEM && PlayerPrefs.GetInt("LeaderboardTutorial", 0) == 0)
            {
                PlayerPrefs.SetInt("LeaderboardTutorial", 1);
                tutorialController.ShowTutorial(tutorialController.leaderboardTutorial);
            }
        }

        string[] time = currentTime.Split(':');
        int hour = int.Parse(time[0]);
        int minute = int.Parse(time[1]);

        minute++;
        if (minute == 60)
        {
            minute = 0;
            hour++;
            if (hour == 24)
            {

                hour = 0;
                if (playerInfo.gameData.currentDay == 5)
                {
                    Time.timeScale = 0;
                    overworldSwitchScene.gameEndPanel.SetActive(true);
                }
                playerInfo.gameData.currentDay++;
                gameDataManager.SaveGameData(playerInfo.gameData);
            }
        }

        currentTime = hour.ToString("00") + ":" + minute.ToString("00");

        timeText.text = currentTime;

        playerInfo.gameData.currentTime = currentTime;
    }
}
