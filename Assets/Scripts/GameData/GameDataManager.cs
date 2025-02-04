using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private string filePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        filePath = Application.persistentDataPath + StaticValues.GAME_DATA_JSON_PATH;
    }

    
    public void CreateNewGameData() {
        int totalNumberOfDays = StaticValues.TOTAL_NUMBER_OF_DAYS;
        if (StaticValues.USE_SKILL_SYSTEM) {
            totalNumberOfDays = (int) (totalNumberOfDays / 2);
        }
        List<DailyGameData> dailyGameDataList = new List<DailyGameData>();
        // Add daily game data
        for (int i = 0; i < totalNumberOfDays; i++) {
            DailyGameData dailyGameData = new DailyGameData {
                homeworkProgress = 0,
                attendance = AttendanceStatus.NOT_STARTED,
            };
            dailyGameDataList.Add(dailyGameData);
        }
        
        GameData newGameData = new GameData {
            playerName = "Player",
            energy = 100,
            hunger = 100,
            stress = 0,
            maneuverability = 0,
            destruction = 0,
            mechanics = 0,
            currentTime = StaticValues.NEW_GAME_START_TIME,
            currentDay = 1,
            totalNumberOfDays = totalNumberOfDays,
            isTutorialEnabled = true,
            dailyGameDataList = dailyGameDataList
        };

        SaveGameData(newGameData);
    }

    public void SaveGameData(GameData gameData) {
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Game data saved: " + filePath);
    }

    public GameData LoadGameData() {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        } 
        return new GameData();
    }
}
