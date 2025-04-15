using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SavedDataManager : MonoBehaviour
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, GameConstants.GAME_DATA_JSON_PATH);

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
        for (int i = 1; i <= 19; i++)
        {
            leaderboard.Add(new LeaderboardEntry
            {
                name = GameConstants.ALIEN_NAMES[i],
                totalScore = 0,
                destructionScore = 0,
                safetyScore = 0
            });
        }

        return leaderboard;
    }

    public static GameData CreateNewGameData()
    {
        return CreateNewGameData("Player");
    }

    public static GameData CreateNewGameData(string playerName)
    {
        int totalNumberOfDays = GameConstants.TOTAL_NUMBER_OF_DAYS;

        List<DailyGameData> dailyGameDataList = new List<DailyGameData>();
        for (int i = 0; i < totalNumberOfDays; i++)
        {
            dailyGameDataList.Add(new DailyGameData
            {
                homeworkProgress = 0,
                attendance = AttendanceStatus.NOT_STARTED
            });
        }

        GameData newGameData = new GameData
        {
            playerName = playerName,
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
            totalScore = 0,
            totalDestructionScore = 0,
            totalSafetyScore = 0,
            activeQuests = new List<Quest>(),
            completedQuestIDs = new List<string>(),
            npcRelationships = new List<NPCRelationshipKeyValuePair>(),
            tutorialsCompleted = new List<string>(),
            openedChests = new List<string>(),
            dailyGameDataList = dailyGameDataList,
            inventory = new List<InventoryItem>(),
            leaderboard = GenerateLeaderboard(playerName),
            moveSpeedBonus = 0,
            minigameMoveSpeedBonus = 0,
            numberOfItemsBought = 0,
            learningSpeedBonus = 0,
            shopItemDiscount = 0,
            totalWorkshopMinutes = 0,
            workshopMoneyBonus = 0,
        };

        SaveGameData(newGameData);
        return newGameData;
    }

    public static void SaveGameData(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData, true);
        try
        {
            File.WriteAllText(FilePath, json);
            Debug.Log("Game data saved to: " + FilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game data: " + e);
        }
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(FilePath))
        {
            try
            {
                Debug.Log("Loading game data from: " + FilePath);
                string json = File.ReadAllText(FilePath);
                return JsonUtility.FromJson<GameData>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("Failed to load game data, creating new: " + e);
                return CreateNewGameData();
            }
        }

        return CreateNewGameData();
    }
}