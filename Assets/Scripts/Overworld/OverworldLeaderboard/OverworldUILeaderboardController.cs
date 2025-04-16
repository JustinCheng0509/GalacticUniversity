using System.Collections.Generic;
using UnityEngine;

public class OverworldUILeaderboardController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    [SerializeField] private GameObject leaderboardPanelUI;
    [SerializeField] private GameObject leaderboardLayoutGroup;
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private Color _highlightColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();    
    }

    public void OpenLeaderboardUI()
    {
        // Clear the previous leaderboard entries
        foreach (Transform child in leaderboardLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        // Get the sorted leaderboard data
        List<LeaderboardEntry> leaderboardEntries = _gameDataManager.ScoreDataManager.GetSortedLeaderboard();

        // Create a new leaderboard entry for each entry in the leaderboard data
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            LeaderboardEntry entry = leaderboardEntries[i];
            GameObject entryObject = Instantiate(leaderboardEntryPrefab, leaderboardLayoutGroup.transform);
            UILeaderboardEntryController entryController = entryObject.GetComponent<UILeaderboardEntryController>();
            entryController.SetEntry(i + 1, entry.name, entry.totalScore);
            // Set the highlight color for the top 3 entries
            if (entry.name == _gameDataManager.PlayerName)
            {
                entryController.SetHighlightColor(_highlightColor);
            }
            // Set parent to leaderboardLayoutGroup
            entryObject.transform.SetParent(leaderboardLayoutGroup.transform, false);
        }

        // Activate the leaderboard panel UI
        leaderboardPanelUI.SetActive(true);
    }

    public void CloseLeaderboardUI()
    {
        // Deactivate the leaderboard panel UI
        leaderboardPanelUI.SetActive(false);
    }
}
