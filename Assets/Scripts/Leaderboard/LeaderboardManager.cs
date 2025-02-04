using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    private string filePath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Application.persistentDataPath + StaticValues.LEADERBOARD_JSON_PATH;    
    }

    public void CreateNewLeaderboard() {
        Leaderboard newLeaderboards = new Leaderboard {
            totalScore = new List<LeaderboardEntry>(),
            destruction = new List<LeaderboardEntry>(),
            safety = new List<LeaderboardEntry>()
        };

        SaveLeaderboard(newLeaderboards);
    }

    public void SaveLeaderboard(Leaderboard leaderboards) {
        string json = JsonUtility.ToJson(leaderboards, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Leaderboard saved: " + filePath);
    }

    public Leaderboard LoadLeaderboard() {
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<Leaderboard>(json);
        } 
        return new Leaderboard();
    }
}
