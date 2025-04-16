using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EndingUIScoreController : MonoBehaviour
{
    // Canvas group
    [SerializeField] private CanvasGroup _canvasGroup;

    // Stat panel
    [SerializeField] private GameObject _scoreUI;
    [SerializeField] private TMP_Text _attendanceText;
    [SerializeField] private TMP_Text _homeworkText;
    [SerializeField] private TMP_Text _damageDealText;
    [SerializeField] private TMP_Text _dangerDestroyedText;
    [SerializeField] private TMP_Text _damageTakenText;
    [SerializeField] private TMP_Text _deathPenaltyText;
    [SerializeField] private TMP_Text _totalScoreText;

    // Leaderboard panel
    [SerializeField] private GameObject _leaderboardLayoutGroup;
    [SerializeField] private GameObject _leaderboardEntryPrefab;
    [SerializeField] private Color _highlightColor;

    private GameDataManager _gameDataManager;

    public event Action OnScoreUIExit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += UpdateScoreUI;
    }

    private void UpdateScoreUI()
    {
        // Update the score UI
        _attendanceText.text = GetAttendancePercentage().ToString("P1");
        _homeworkText.text = GetHomeworkPercentage().ToString("P1");
        _damageDealText.text = _gameDataManager.ScoreDataManager.TotalDamageDealt.ToString();
        _dangerDestroyedText.text = _gameDataManager.ScoreDataManager.DangersDestroyedScore.ToString();
        _damageTakenText.text = _gameDataManager.ScoreDataManager.TotalDamageTaken.ToString();
        _deathPenaltyText.text = (_gameDataManager.ScoreDataManager.TimesDead * MinigameConstants.MINIGAME_PLAYER_BASE_DEATH_PENALTY).ToString();
        _totalScoreText.text = _gameDataManager.ScoreDataManager.TotalScore.ToString();

        // Update the leaderboard
        UpdateLeaderboard();
    }

    private float GetAttendancePercentage()
    {
        int attendanceCount = _gameDataManager.DailyGameDataList.Count(x => x.attendance == AttendanceStatus.ATTENDED);
        return (float) attendanceCount / _gameDataManager.TotalNumberOfDays;
    }

    private float GetHomeworkPercentage()
    {
        float totalHomeworkProgress = _gameDataManager.DailyGameDataList.Sum(x => x.homeworkProgress);
        return totalHomeworkProgress / _gameDataManager.TotalNumberOfDays;
    }

    private void UpdateLeaderboard()
    {
        // Clear the existing leaderboard entries
        foreach (Transform child in _leaderboardLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }

        List<LeaderboardEntry> leaderboardEntries = _gameDataManager.ScoreDataManager.GetSortedLeaderboard();

         // Create a new leaderboard entry for each entry in the leaderboard data
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            LeaderboardEntry entry = leaderboardEntries[i];
            GameObject entryObject = Instantiate(_leaderboardEntryPrefab, _leaderboardLayoutGroup.transform);
            UILeaderboardEntryController entryController = entryObject.GetComponent<UILeaderboardEntryController>();
            entryController.SetEntry(i + 1, entry.name, entry.totalScore);
            // Set the highlight color for the top 3 entries
            if (entry.name == _gameDataManager.PlayerName)
            {
                entryController.SetHighlightColor(_highlightColor);
            }
            // Set parent to leaderboardLayoutGroup
            entryObject.transform.SetParent(_leaderboardLayoutGroup.transform, false);
        }
    }

    public void FadeInScoreUI()
    {
        // Start the fade-in coroutine
        StartCoroutine(FadeInScoreUICoroutine());
    }

    private IEnumerator FadeInScoreUICoroutine()
    {
        _canvasGroup.alpha = 0;
        _scoreUI.SetActive(true);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime / 1f; // Adjust the duration as needed
            yield return null;
        }

        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void CloseScoreUI()
    {
        _scoreUI.SetActive(false);
        OnScoreUIExit?.Invoke();
    }
}
