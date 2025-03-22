using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MinigameUIScoreController : MonoBehaviour
{
    private MinigameScoreController _minigameScoreController;

    [SerializeField] private GameObject _gameEndPanel;

    [SerializeField] private TMP_Text _dayCompleteText;
    [SerializeField] private TMP_Text _baseScoreText;
    [SerializeField] private TMP_Text _damageDealtScoreText;
    [SerializeField] private TMP_Text _dangersDestroyedScoreText;
    [SerializeField] private TMP_Text _damageTakenScoreText;
    [SerializeField] private TMP_Text _timesDeadScoreText;
    [SerializeField] private TMP_Text _todayScoreText;
    [SerializeField] private TMP_Text _previousTotalScoreText;
    [SerializeField] private TMP_Text _newTotalScoreText;

    [SerializeField] private GameObject _totalScoreLeaderboardPanel;
    [SerializeField] private GameObject _destructionScoreLeaderboardPanel;
    [SerializeField] private GameObject _safetyScoreLeaderboardPanel;

    void Start()
    {
        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        _minigameScoreController.OnMinigameEndScoreCalculated += UpdateScoreUI;
        _minigameScoreController.OnLeaderboardUpdated += UpdateLeaderboardUI;
    }

    public void UpdateScoreUI(int currentDay, int deathPenaltyScore, int previousTotalScore, int newTotalScore)
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

    private void UpdateLeaderboardUI(List<LeaderboardEntry> leaderboard, string playerName)
    {
        // Sort leaderboards separately
        List<LeaderboardEntry> totalScoreLeaderboard = leaderboard.OrderByDescending(x => x.totalScore).ToList();
        List<LeaderboardEntry> destructionScoreLeaderboard = leaderboard.OrderByDescending(x => x.destructionScore).ToList();
        List<LeaderboardEntry> safetyScoreLeaderboard = leaderboard.OrderBy(x => x.safetyScore).ToList(); // Lower safety score = better rank

        // Find the player's ranking in each leaderboard and get the 3 relevant positions
        List<int> totalScorePositions = GetRelevantPositions(totalScoreLeaderboard, playerName);
        List<int> destructionScorePositions = GetRelevantPositions(destructionScoreLeaderboard, playerName);
        List<int> safetyScorePositions = GetRelevantPositions(safetyScoreLeaderboard, playerName);

        // Update UI panels
        // UpdateLeaderboardPanel(_totalScoreLeaderboardPanel, totalScoreLeaderboard, totalScorePositions);
        // UpdateLeaderboardPanel(_destructionScoreLeaderboardPanel, destructionScoreLeaderboard, destructionScorePositions);
        // UpdateLeaderboardPanel(_safetyScoreLeaderboardPanel, safetyScoreLeaderboard, safetyScorePositions);

        _gameEndPanel.SetActive(true);
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
            entryUI.transform.Find("ScoreText").GetComponent<TMP_Text>().text = GetScoreText(sortedLeaderboard[posIndex], panel);
        }
    }
    private string GetScoreText(LeaderboardEntry entry, GameObject panel)
    {
        if (panel == _totalScoreLeaderboardPanel)
            return entry.totalScore.ToString();
        else if (panel == _destructionScoreLeaderboardPanel)
            return entry.destructionScore.ToString();
        else
            return entry.safetyScore.ToString();
    }
}
