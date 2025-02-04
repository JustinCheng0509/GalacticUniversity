using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Leaderboard
{
    public List<LeaderboardEntry> totalScore = new List<LeaderboardEntry>();
    public List<LeaderboardEntry> destruction = new List<LeaderboardEntry>();
    public List<LeaderboardEntry> safety = new List<LeaderboardEntry>();
}
