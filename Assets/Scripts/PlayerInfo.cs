using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInfo: MonoBehaviour
{
    
    public GameData gameData = new GameData();
    public Leaderboard leaderboard = new Leaderboard();

    public bool isSleeping;
    public bool isDoingHomework;
    public bool isWorking;
    public bool isPlaying;

    [SerializeField] private TMP_Text energyText;
    [SerializeField] private TMP_Text hungerText;
    [SerializeField] private TMP_Text stressText;

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
        energyText.text = "Energy: " + Mathf.RoundToInt(gameData.energy).ToString();
        hungerText.text = "Hunger: " + Mathf.RoundToInt(gameData.hunger).ToString();
        stressText.text = "Stress: " + Mathf.RoundToInt(gameData.stress).ToString();
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
                gameData.stress += 0.2f;
                if (isDoingHomework && gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress < 100) {
                   gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress += 1f;
                    overworldUIController.UpdateHomeworkProgress(gameData.dailyGameDataList[gameData.currentDay-1].homeworkProgress);
                }
            }
            if (isPlaying){
                gameData.stress -= 0.2f;
            }
        }
    }

    public float GetHomeworkProgress()
    {
        // Debug.Log(gameData.dailyGameDataList.Count);
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
