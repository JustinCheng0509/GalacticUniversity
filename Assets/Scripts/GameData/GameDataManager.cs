using System.Collections.Generic;
using System.IO;
using Defective.JSON;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // private string filePath;
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {   
    //     filePath = Application.persistentDataPath + StaticValues.GAME_DATA_JSON_PATH;
    // }

    public List<LeaderboardEntry> GenerateLeaderboard(string playerName)
    {
        List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
        leaderboard.Add(new LeaderboardEntry {
            name = playerName,
            totalScore = 0,
            destructionScore = 0,
            safetyScore = 0
        });
        for (int i = 1 ; i <= 19; i++) {
            leaderboard.Add(new LeaderboardEntry {
                name = StaticValues.ALIEN_NAMES[i],
                totalScore = 0,
                destructionScore = 0,
                safetyScore = 0
            });
        }

        return leaderboard;
    }

    
    public GameData CreateNewGameData() {
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
            mood = 100,
            money = 200,
            maneuverability = 0,
            destruction = 0,
            mechanics = 0,
            currentTime = StaticValues.NEW_GAME_START_TIME,
            currentDay = 1,
            totalNumberOfDays = totalNumberOfDays,
            isTutorialEnabled = true,
            totalScore = 0,
            totalDestructionScore = 0,
            totalSafetyScore = 0,
            dailyGameDataList = dailyGameDataList,
            leaderboard = GenerateLeaderboard("Player")
        };

        SaveGameData(newGameData);
        return newGameData;
    }

    public void SaveGameData(GameData gameData) {
        // string json = JsonUtility.ToJson(gameData, true);
        // File.WriteAllText(filePath, json);
        // Debug.Log("Game data saved: " + filePath);

        PlayerPrefs.SetString(StaticValues.GAME_DATA_KEY, JsonUtility.ToJson(gameData));
    }

    public GameData LoadGameData() {
        // if (File.Exists(filePath)) {
        //     string json = File.ReadAllText(filePath);
        //     return JsonUtility.FromJson<GameData>(json);
        // } 

        if (PlayerPrefs.HasKey(StaticValues.GAME_DATA_KEY)) {
            GameData gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(StaticValues.GAME_DATA_KEY));

            // Debug.Log("Game data " + gameData.dailyGameDataList.Count + " loaded from PlayerPrefs");

            return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(StaticValues.GAME_DATA_KEY));
        }

        return CreateNewGameData();
    }
}
