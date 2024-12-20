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

    [SerializeField]
    private OverworldSwitchScene overworldSwitchScene;

    public bool canAttendClass
    {
        get
        {
            // between 13:45 and 14:15
            string[] time = currentTime.Split(':');
            int hour = int.Parse(time[0]);

            return hour >= 14 && hour < 16;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("currentTime"))
        {
            currentTime = PlayerPrefs.GetString("currentTime");
        }
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
            yield return new WaitForSeconds(interval);
            UpdateTime();
        }
    }

    void UpdateTime()
    {
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
                Time.timeScale = 0;
                overworldSwitchScene.gameEndPanel.SetActive(true);
            }
        }

        currentTime = hour.ToString("00") + ":" + minute.ToString("00");

        timeText.text = currentTime;

        // Save the time to PlayerPrefs
        PlayerPrefs.SetString("currentTime", currentTime);
    }
}
