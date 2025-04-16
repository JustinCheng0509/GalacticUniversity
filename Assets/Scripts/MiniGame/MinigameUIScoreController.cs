using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameUIScoreController : MonoBehaviour
{
    private MinigameScoreController _minigameScoreController;
    private GameDataManager _gameDataManager;

    [SerializeField] private GameObject _gameEndPanel;
    [SerializeField] private TMP_Text _dayCompleteText;
    [SerializeField] private TMP_Text _damageDealtScoreText;
    [SerializeField] private TMP_Text _dangersDestroyedScoreText;
    [SerializeField] private TMP_Text _damageTakenScoreText;
    [SerializeField] private TMP_Text _deathPenaltyScoreText;
    [SerializeField] private TMP_Text _todayScoreText;
    [SerializeField] private GameObject _totalScoreLeaderboardPanel;

    void Start()
    {
        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        _minigameScoreController.OnMinigameEndScoreCalculated += UpdateUI;
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    public void UpdateUI()
    {
        _dayCompleteText.text = "Day " + _gameDataManager.CurrentDay + " Complete!";
        _damageDealtScoreText.text = _minigameScoreController.DamageDealt.ToString();
        _dangersDestroyedScoreText.text = _minigameScoreController.DangersDestroyedScore.ToString();
        _damageTakenScoreText.text = "- " + _minigameScoreController.DamageTaken.ToString();
        _deathPenaltyScoreText.text = "- " + _minigameScoreController.DeathPenaltyScore.ToString();
        _todayScoreText.text = _minigameScoreController.TotalScoreThisRound.ToString();
        UpdateLeaderboardUI();
        
        _gameEndPanel.SetActive(true);
    }

    private void UpdateLeaderboardUI()
    {
        // Sort leaderboards separately
        List<LeaderboardEntry> totalScoreLeaderboard = _gameDataManager.ScoreDataManager.GetSortedLeaderboard();

        // Find the player's ranking in each leaderboard and get the 3 relevant positions
        List<int> totalScorePositions = GetRelevantPositions(totalScoreLeaderboard, _gameDataManager.PlayerName);

        // Update UI panels
        UpdateLeaderboardPanel(_totalScoreLeaderboardPanel, totalScoreLeaderboard, totalScorePositions);

    }

    private List<int> GetRelevantPositions(List<LeaderboardEntry> sortedLeaderboard, string playerName)
    {
        int playerIndex = sortedLeaderboard.FindIndex(entry => entry.name == playerName);
        List<int> positions = new List<int>();

        if (playerIndex == 0) // Player is first
        {
            for (int i = 0; i < Math.Min(3, sortedLeaderboard.Count); i++)
                positions.Add(i);
        }
        else if (playerIndex == sortedLeaderboard.Count - 1) // Player is last
        {
            for (int i = Math.Max(0, sortedLeaderboard.Count - 3); i < sortedLeaderboard.Count; i++)
                positions.Add(i);
        }
        else // Player is in the middle
        {
            positions.Add(Math.Max(0, playerIndex - 1)); // One above
            positions.Add(playerIndex); // Player
            positions.Add(Math.Min(sortedLeaderboard.Count - 1, playerIndex + 1)); // One below
        }

        return positions;
    }

    private void UpdateLeaderboardPanel(GameObject panel, List<LeaderboardEntry> sortedLeaderboard, List<int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            int posIndex = positions[i];
            GameObject entryUI = panel.transform.Find("Pos" + (i + 1).ToString()).gameObject;

            // Update UI elements
            entryUI.transform.Find("PositionLabel").GetComponent<TMP_Text>().text = (posIndex + 1).ToString() + ".";
            entryUI.transform.Find("ScoreLabel").GetComponent<TMP_Text>().text = sortedLeaderboard[posIndex].name;
            entryUI.transform.Find("ScoreText").GetComponent<TMP_Text>().text = GetScoreText(sortedLeaderboard[posIndex]);
        }
    }
    private string GetScoreText(LeaderboardEntry entry)
    {
        return entry.totalScore.ToString();
    }
}
