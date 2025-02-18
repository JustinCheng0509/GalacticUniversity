using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameUIScoreController : MonoBehaviour
{
    private MinigameScoreController _minigameScoreController;

    [SerializeField] private TMP_Text _dayCompleteText;
    [SerializeField] private TMP_Text _baseScoreText;
    [SerializeField] private TMP_Text _damageDealtScoreText;
    [SerializeField] private TMP_Text _dangersDestroyedScoreText;
    [SerializeField] private TMP_Text _damageTakenScoreText;
    [SerializeField] private TMP_Text _timesDeadScoreText;
    [SerializeField] private TMP_Text _todayScoreText;
    [SerializeField] private TMP_Text _previousTotalScoreText;
    [SerializeField] private TMP_Text _newTotalScoreText;

    // [SerializeField] private PlayerShipInfo playerShipInfo;

    [SerializeField] private GameObject _totalScoreLeaderboardPanel;
    [SerializeField] private GameObject _destructionScoreLeaderboardPanel;
    [SerializeField] private GameObject _safetyScoreLeaderboardPanel;

    void Start()
    {
        _minigameScoreController = FindFirstObjectByType<MinigameScoreController>();
        _minigameScoreController.OnMinigameEndScoreCalculated += UpdateScorePanel;
    }

    public void UpdateScorePanel(int currentDay, int deathPenaltyScore, int previousTotalScore, int newTotalScore)
    {
        _dayCompleteText.text = "Day " + currentDay + " Complete!";
        _baseScoreText.text = _minigameScoreController.BaseScore.ToString();
        _damageDealtScoreText.text = _minigameScoreController.DamageDealt.ToString();
        _dangersDestroyedScoreText.text = _minigameScoreController.DangersDestroyedScore.ToString();
        _damageTakenScoreText.text = "- " + _minigameScoreController.DamageTaken.ToString();
        _timesDeadScoreText.text = "- " + deathPenaltyScore.ToString();
        _todayScoreText.text = _minigameScoreController.TotalScoreThisRound.ToString();
        _previousTotalScoreText.text = previousTotalScore.ToString();
        _newTotalScoreText.text = newTotalScore.ToString();
    }

    // public void UpdateScorePanel(int baseScore, int damageDealtScore, int dangersDestroyedScore, int damageTakenScore, int timesDeadScore)
    // {
    //     dayCompleteText.text = "Day " + playerShipInfo.gameData.currentDay + " Complete!";
    //     baseScoreText.text = baseScore.ToString();

    //     damageDealtScoreText.text = damageDealtScore.ToString();
    //     dangersDestroyedScoreText.text = dangersDestroyedScore.ToString();  
    //     playerShipInfo.gameData.totalDestructionScore = playerShipInfo.gameData.totalDestructionScore + damageDealtScore + dangersDestroyedScore;

    //     damageTakenScoreText.text = "- " + damageTakenScore.ToString();
    //     timesDeadScoreText.text = "- " + timesDeadScore.ToString();
    //     playerShipInfo.gameData.totalSafetyScore = playerShipInfo.gameData.totalSafetyScore + damageTakenScore + timesDeadScore;

    //     int todayScore = damageDealtScore + dangersDestroyedScore - damageTakenScore - timesDeadScore;
    //     todayScoreText.text = todayScore.ToString();
        
    //     int previousTotalScore = playerShipInfo.gameData.totalScore;
    //     previousTotalScoreText.text = previousTotalScore.ToString();

    //     int newTotalScore = baseScore + previousTotalScore + todayScore;
    //     playerShipInfo.gameData.totalScore = newTotalScore;
    //     newTotalScoreText.text = newTotalScore.ToString();

    //     CalculateLeaderboard(baseScore, damageDealtScore, dangersDestroyedScore, damageTakenScore, timesDeadScore);
    //     UpdateLeaderboard();
    // }

    // private void CalculateLeaderboard(int baseScore, int damageDealtScore, int dangersDestroyedScore, int damageTakenScore, int timesDeadScore)
    // {
    //     List<LeaderboardEntry> leaderboard = playerShipInfo.gameData.leaderboard;
    //     // Find the player's entry in the leaderboard and update it
    //     for (int i = 0; i < leaderboard.Count; i++)
    //     {
    //         Debug.Log(leaderboard[i].name);
    //         if (leaderboard[i].name == playerShipInfo.gameData.playerName)
    //         {
    //             leaderboard[i].totalScore = playerShipInfo.gameData.totalScore;
    //             leaderboard[i].destructionScore = playerShipInfo.gameData.totalDestructionScore;
    //             leaderboard[i].safetyScore = playerShipInfo.gameData.totalSafetyScore;
    //         } else {
    //             int newScore = baseScore;
    //             int destructionScore = (int)((damageDealtScore + 500)*Random.Range(0.6f, 1.2f)) + (int)((dangersDestroyedScore + 200)*Random.Range(0.6f, 1.2f));
    //             int safetyScore = (int)((damageTakenScore + 100)*Random.Range(0.6f, 1.2f)) + (int)(timesDeadScore*Random.Range(0.6f, 1.2f));
    //             Debug.Log("New Score: " + newScore);
    //             Debug.Log("Destruction Score: " + destructionScore);
    //             Debug.Log("Safety Score: " + safetyScore);
    //             newScore += destructionScore;
    //             newScore -= safetyScore;
    //             leaderboard[i].totalScore += newScore;
    //             leaderboard[i].destructionScore += destructionScore;
    //             leaderboard[i].safetyScore += safetyScore;
    //         }
    //     }
    //     playerShipInfo.gameData.leaderboard = leaderboard;
    // }

    // private void UpdateLeaderboard()
    // {
    //     List<LeaderboardEntry> totalScoreLeaderboard = new List<LeaderboardEntry>(playerShipInfo.gameData.leaderboard);
    //     List<LeaderboardEntry> destructionScoreLeaderboard = new List<LeaderboardEntry>(playerShipInfo.gameData.leaderboard);
    //     List<LeaderboardEntry> safetyScoreLeaderboard = new List<LeaderboardEntry>(playerShipInfo.gameData.leaderboard);
    //     List<int> playerTotalScoreLeaderboard = new List<int>();
    //     List<int> playerDestructionScoreLeaderboard = new List<int>();
    //     List<int> playerSafetyScoreLeaderboard = new List<int>();
    //     totalScoreLeaderboard.Sort((x, y) => y.totalScore.CompareTo(x.totalScore));
    //     for (int i = 0; i < totalScoreLeaderboard.Count; i++)
    //     {
    //         // Find the player's entry
    //         if (totalScoreLeaderboard[i].name == playerShipInfo.gameData.playerName)
    //         {
    //             // Check if the player is the first place
    //             if (i == 0) {
    //                 // Add the player's entry to the playerTotalScoreLeaderboard and the 2 positions below
    //                 playerTotalScoreLeaderboard.Add(i);
    //                 playerTotalScoreLeaderboard.Add(i+1);
    //                 playerTotalScoreLeaderboard.Add(i+2);
    //             } else if (i == totalScoreLeaderboard.Count - 1) {
    //                 // Add the 2 positions above the player's entry and the player's entry to the playerTotalScoreLeaderboard
    //                 playerTotalScoreLeaderboard.Add(i-2);
    //                 playerTotalScoreLeaderboard.Add(i-1);
    //                 playerTotalScoreLeaderboard.Add(i);
    //             } else {
    //                 // Add 1 position above the player's entry, the player's entry and 1 position below the player's entry to the playerTotalScoreLeaderboard
    //                 playerTotalScoreLeaderboard.Add(i-1);
    //                 playerTotalScoreLeaderboard.Add(i);
    //                 playerTotalScoreLeaderboard.Add(i+1);
    //             }
    //             break;
    //         }
    //     }

    //     destructionScoreLeaderboard.Sort((x, y) => y.destructionScore.CompareTo(x.destructionScore));
    //     for (int i = 0; i < destructionScoreLeaderboard.Count; i++)
    //     {
    //         // Find the player's entry
    //         if (destructionScoreLeaderboard[i].name == playerShipInfo.gameData.playerName)
    //         {
    //             // Check if the player is the first place
    //             if (i == 0) {
    //                 // Add the player's entry to the playerDestructionScoreLeaderboard and the 2 positions below
    //                 playerDestructionScoreLeaderboard.Add(i);
    //                 playerDestructionScoreLeaderboard.Add(i+1);
    //                 playerDestructionScoreLeaderboard.Add(i+2);
    //             } else if (i == destructionScoreLeaderboard.Count - 1) {
    //                 // Add the 2 positions above the player's entry and the player's entry to the playerDestructionScoreLeaderboard
    //                 playerDestructionScoreLeaderboard.Add(i-2);
    //                 playerDestructionScoreLeaderboard.Add(i-1);
    //                 playerDestructionScoreLeaderboard.Add(i);
    //             } else {
    //                 // Add 1 position above the player's entry, the player's entry and 1 position below the player's entry to the playerDestructionScoreLeaderboard
    //                 playerDestructionScoreLeaderboard.Add(i-1);
    //                 playerDestructionScoreLeaderboard.Add(i);
    //                 playerDestructionScoreLeaderboard.Add(i+1);
    //             }
    //             break;
    //         }
    //     }

    //     safetyScoreLeaderboard.Sort((y, x) => y.safetyScore.CompareTo(x.safetyScore));
    //     for (int i = 0; i < safetyScoreLeaderboard.Count; i++)
    //     {
    //         // Find the player's entry
    //         if (safetyScoreLeaderboard[i].name == playerShipInfo.gameData.playerName)
    //         {
    //             // Check if the player is the first place
    //             if (i == 0) {
    //                 // Add the player's entry to the playerSafetyScoreLeaderboard and the 2 positions below
    //                 playerSafetyScoreLeaderboard.Add(i);
    //                 playerSafetyScoreLeaderboard.Add(i+1);
    //                 playerSafetyScoreLeaderboard.Add(i+2);
    //             } else if (i == safetyScoreLeaderboard.Count - 1) {
    //                 // Add the 2 positions above the player's entry and the player's entry to the playerSafetyScoreLeaderboard
    //                 playerSafetyScoreLeaderboard.Add(i-2);
    //                 playerSafetyScoreLeaderboard.Add(i-1);
    //                 playerSafetyScoreLeaderboard.Add(i);
    //             } else {
    //                 // Add 1 position above the player's entry, the player's entry and 1 position below the player's entry to the playerSafetyScoreLeaderboard
    //                 playerSafetyScoreLeaderboard.Add(i-1);
    //                 playerSafetyScoreLeaderboard.Add(i);
    //                 playerSafetyScoreLeaderboard.Add(i+1);
    //             }
    //             break;
    //         }
    //     }

    //     // Debug.Log(playerTotalScoreLeaderboard.Count);
    //     // Debug.Log(playerDestructionScoreLeaderboard.Count);
    //     // Debug.Log(playerSafetyScoreLeaderboard.Count);

    //     // Update the Total Score Leaderboard
    //     for (int i = 0; i < playerTotalScoreLeaderboard.Count; i++)
    //     {
    //         // Find the HigerPos child object
    //         GameObject higherPos = totalScoreLeaderboardPanel.transform.Find("Pos" + (i+1).ToString()).gameObject;
    //         // Find the PositionLabel child object and change the TMP_Text to player's position
    //         higherPos.transform.Find("PositionLabel").GetComponent<TMP_Text>().text = (playerTotalScoreLeaderboard[i]+1).ToString() + ".";
    //         // Find the ScoreLabel child object and change the TMP_Text to player's name
    //         higherPos.transform.Find("ScoreLabel").GetComponent<TMP_Text>().text = totalScoreLeaderboard[playerTotalScoreLeaderboard[i]].name;
    //         // Find the ScoreText child object and change the TMP_Text to player's total score
    //         higherPos.transform.Find("ScoreText").GetComponent<TMP_Text>().text = totalScoreLeaderboard[playerTotalScoreLeaderboard[i]].totalScore.ToString();
    //     }

    //     // Update the Destruction Score Leaderboard
    //     for (int i = 0; i < playerDestructionScoreLeaderboard.Count; i++)
    //     {
    //         // Find the HigerPos child object
    //         GameObject higherPos = destructionScoreLeaderboardPanel.transform.Find("Pos" + (i+1).ToString()).gameObject;
    //         // Find the PositionLabel child object and change the TMP_Text to player's position
    //         higherPos.transform.Find("PositionLabel").GetComponent<TMP_Text>().text = (playerDestructionScoreLeaderboard[i]+1).ToString() + ".";
    //         // Find the ScoreLabel child object and change the TMP_Text to player's name
    //         higherPos.transform.Find("ScoreLabel").GetComponent<TMP_Text>().text = destructionScoreLeaderboard[playerDestructionScoreLeaderboard[i]].name;
    //         // Find the ScoreText child object and change the TMP_Text to player's destruction score
    //         higherPos.transform.Find("ScoreText").GetComponent<TMP_Text>().text = destructionScoreLeaderboard[playerDestructionScoreLeaderboard[i]].destructionScore.ToString();
    //     }

    //     // Update the Safety Score Leaderboard
    //     for (int i = 0; i < playerSafetyScoreLeaderboard.Count; i++)
    //     {
    //         // Find the HigerPos child object
    //         GameObject higherPos = safetyScoreLeaderboardPanel.transform.Find("Pos" + (i+1).ToString()).gameObject;
    //         // Find the PositionLabel child object and change the TMP_Text to player's position
    //         higherPos.transform.Find("PositionLabel").GetComponent<TMP_Text>().text = (playerSafetyScoreLeaderboard[i]+1).ToString() + ".";
    //         // Find the ScoreLabel child object and change the TMP_Text to player's name
    //         higherPos.transform.Find("ScoreLabel").GetComponent<TMP_Text>().text = safetyScoreLeaderboard[playerSafetyScoreLeaderboard[i]].name;
    //         // Find the ScoreText child object and change the TMP_Text to player's safety score
    //         higherPos.transform.Find("ScoreText").GetComponent<TMP_Text>().text = safetyScoreLeaderboard[playerSafetyScoreLeaderboard[i]].safetyScore.ToString();
    //     }
    // }
}
