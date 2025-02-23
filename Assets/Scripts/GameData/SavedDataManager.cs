using System.Collections.Generic;
using System.IO;
using Defective.JSON;
using UnityEngine;

public class SavedDataManager : MonoBehaviour
{
    // private string filePath;
    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {   
    //     filePath = Application.persistentDataPath + GameConstants.GAME_DATA_JSON_PATH;
    // }

    public static List<LeaderboardEntry> GenerateLeaderboard(string playerName)
    {
        List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>
        {
            new LeaderboardEntry
            {
                name = playerName,
                totalScore = 0,
                destructionScore = 0,
                safetyScore = 0
            }
        };
        for (int i = 1 ; i <= 19; i++) {
            leaderboard.Add(new LeaderboardEntry {
                name = GameConstants.ALIEN_NAMES[i],
                totalScore = 0,
                destructionScore = 0,
                safetyScore = 0
            });
        }

        return leaderboard;
    }

    
    public static GameData CreateNewGameData() {
        int totalNumberOfDays = GameConstants.TOTAL_NUMBER_OF_DAYS;
        
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
            currentTime = GameConstants.NEW_GAME_START_TIME,
            currentDay = 1,
            totalNumberOfDays = totalNumberOfDays,
            isTutorialEnabled = true,
            introDialogPlayed = false,
            introClassPlayed = false,
            introDormPlayed = false,
            introShopPlayed = false,
            introWorkPlayed = false,
            introPlayRoomPlayed = false,
            totalScore = 0,
            totalDestructionScore = 0,
            totalSafetyScore = 0,
            activeQuests = new List<Quest>(),
            tutorialsCompleted = new List<string>(),
            dailyGameDataList = dailyGameDataList,
            inventory = new List<Item>(),
            leaderboard = GenerateLeaderboard("Player")
        };

        SaveGameData(newGameData);
        return newGameData;
    }

    public static void SaveGameData(GameData gameData) {
        // string json = JsonUtility.ToJson(gameData, true);
        // File.WriteAllText(filePath, json);
        // Debug.Log("Game data saved: " + filePath);

        PlayerPrefs.SetString(GameConstants.GAME_DATA_KEY, JsonUtility.ToJson(gameData));
    }

    public static GameData LoadGameData() {
        // if (File.Exists(filePath)) {
        //     string json = File.ReadAllText(filePath);
        //     return JsonUtility.FromJson<GameData>(json);
        // } 

        if (PlayerPrefs.HasKey(GameConstants.GAME_DATA_KEY)) {
            GameData gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(GameConstants.GAME_DATA_KEY));

            // Debug.Log("Game data " + gameData.dailyGameDataList.Count + " loaded from PlayerPrefs");

            return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(GameConstants.GAME_DATA_KEY));
        }

        return CreateNewGameData();
    }
}
