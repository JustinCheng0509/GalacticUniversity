using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInfo: MonoBehaviour
{
    
    public GameData gameData = new GameData();

    public bool isSleeping;
    public bool isDoingHomework;
    public bool isWorking;
    public bool isPlaying;

    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text hungerText;
    [SerializeField] private TMP_Text moodText;

    [SerializeField] private OverworldTimeController overworldTimeController;

    IEnumerator updateStatsCoroutine;

    [SerializeField]
    private GameObject interactStatusPanel;

    [SerializeField]
    private TMP_Text interactStatusText;

    [SerializeField]
    private OverworldUIController overworldUIController;

    public void Start()
    {
        updateStatsCoroutine = UpdateStats();
        StartCoroutine(updateStatsCoroutine);
    }

    private void Update() {
        // Update the UI, Format to int
        energyText.text = Mathf.RoundToInt(gameData.energy).ToString();
        hungerText.text = Mathf.RoundToInt(gameData.hunger).ToString();
        moodText.text = Mathf.RoundToInt(gameData.mood).ToString();
    }

    private IEnumerator UpdateStats()
    {
        while (true)
        {
            float timeInterval = overworldTimeController.intervalBetweenMinute;
            yield return new WaitForSeconds(timeInterval);
            if (!isSleeping){
                gameData.energy -= 0.05f;
            } else {
                gameData.energy += 0.15f;
            }
            gameData.hunger -= 0.05f;
            if (isWorking || isDoingHomework){
                gameData.mood -= 0.2f;
                if (isDoingHomework && gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress < 100) {
                   gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress += 1f;
                    overworldUIController.UpdateHomeworkProgress(gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress);
                    // If using skill system, add skill points here
                    if (StaticValues.USE_SKILL_SYSTEM) {
                        // 5% chance to increase one of the skills
                        if (Random.Range(0, 100) < 5) {
                            int skillToIncrease = Random.Range(0, 3);
                            switch (skillToIncrease) {
                                case 0:
                                    gameData.maneuverability += 1;
                                    break;
                                case 1:
                                    gameData.destruction += 1;
                                    break;
                                case 2:
                                    gameData.mechanics += 1;
                                    break;
                            }
                        }
                    }
                }
                // if working, add money
                if (isWorking) {
                    gameData.money += 1;
                    // 5% chance to increase one of the skills
                    if (Random.Range(0, 100) < 5) {
                        int skillToIncrease = Random.Range(0, 3);
                        switch (skillToIncrease) {
                            case 0:
                                gameData.maneuverability += 1;
                                break;
                            case 1:
                                gameData.destruction += 1;
                                break;
                            case 2:
                                gameData.mechanics += 1;
                                break;
                        }
                    }
                }
            }
            if (isPlaying){
                gameData.mood += 0.2f;
            }
        }
    }

    public float GetHomeworkProgress()
    {
        return gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress;
    }

    public AttendanceStatus GetAttendanceStatus()
    {
        return gameData.dailyGameDataList[gameData.currentDay-1].attendance;
    }

    public void SetAttendanceStatus(AttendanceStatus attendanceStatus)
    {
        gameData.dailyGameDataList[gameData.currentDay-1].attendance = attendanceStatus;
    }

    public bool IsBusy()
    {
        return isSleeping || isDoingHomework || isWorking || isPlaying;
    }

    public void CancelActions()
    {
        interactStatusText.text = "";
        interactStatusPanel.SetActive(false);
        isSleeping = false;
        isDoingHomework = false;
        isWorking = false;
        isPlaying = false;
    }
}
