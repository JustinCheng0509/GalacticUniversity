using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private float _damageDealt = 0f;
    private float _damageTaken = 0f;
    private float _dangersDestroyedScore = 0f;
    private int _timesDead = 0;
    private float _deathPenaltyScore = 0f;
    private float _totalScoreThisRound = 0f;

    public event Action OnMinigameEndScoreCalculated;

    private GameDataManager _gameDataManager;
    
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    public float TotalScoreThisRound
    {
        get => _totalScoreThisRound;
        set
        {
            _totalScoreThisRound = value;
            _scoreText.text = "Score: " + Mathf.RoundToInt(_totalScoreThisRound).ToString();
        }
    }

    public float DamageDealt
    {
        get => _damageDealt;
        set
        {
            _damageDealt = value;
            RecalculateScore();
        }
    }

    public float DamageTaken
    {
        get => _damageTaken;
        set
        {
            _damageTaken = value;
            RecalculateScore();
        }
    }

    public float DangersDestroyedScore
    {
        get => _dangersDestroyedScore;
        set
        {
            _dangersDestroyedScore = value;
            RecalculateScore();
        }
    }

    public int TimesDead
    {
        get => _timesDead;
        set
        {
            _timesDead = value;
            RecalculateScore();
        }
    }

    public float DeathPenaltyScore => _deathPenaltyScore;

    private void RecalculateScore()
    {
        _deathPenaltyScore = _timesDead * MinigameConstants.MINIGAME_PLAYER_BASE_DEATH_PENALTY;
        TotalScoreThisRound = _damageDealt - _damageTaken - _deathPenaltyScore + _dangersDestroyedScore;
    }

    public void CalculateFinalScore()
    {
        _damageDealt = Mathf.RoundToInt(_damageDealt);
        _damageTaken = Mathf.RoundToInt(_damageTaken);
        _dangersDestroyedScore = Mathf.RoundToInt(_dangersDestroyedScore);
        RecalculateScore();
        
        // Update GameDataManager
        _gameDataManager.ScoreDataManager.TotalDamageDealt += (int) _damageDealt;
        _gameDataManager.ScoreDataManager.TotalDamageTaken += (int) _damageTaken;
        _gameDataManager.ScoreDataManager.DangersDestroyedScore += (int) _dangersDestroyedScore;
        _gameDataManager.ScoreDataManager.TimesDead += _timesDead;
        _gameDataManager.ScoreDataManager.TotalScore += (int) _totalScoreThisRound;
        _gameDataManager.GenerateLeaderboardScore();
        OnMinigameEndScoreCalculated?.Invoke();
    }
}
