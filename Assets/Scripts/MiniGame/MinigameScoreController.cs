using System;
using UnityEngine;

public class MinigameScoreController : MonoBehaviour
{
    private float _score = 0f;
    private float _baseScore = 3000f;
    private float _damageDealt = 0f;
    private float _damageTaken = 0f;
    private float _dangersDestroyedScore = 0f;
    private int _timesDead = 0;
    private float _baseDeathPenalty = 500f;


    private int _totalScoreThisRound = 0;

    public event Action<int, int, int, int> OnMinigameEndScoreCalculated;

    private GameDataManager _gameDataManager;
    
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    public float Score
    {
        get => _score;
        set
        {
            _score = value;
        }
    }

    public float BaseScore => _baseScore;

    public float DamageDealt
    {
        get => _damageDealt;
        set
        {
            _damageDealt = value;
        }
    }

    public float DamageTaken
    {
        get => _damageTaken;
        set
        {
            _damageTaken = value;
        }
    }

    public float DangersDestroyedScore
    {
        get => _dangersDestroyedScore;
        set
        {
            _dangersDestroyedScore = value;
        }
    }

    public int TimesDead
    {
        get => _timesDead;
        set
        {
            _timesDead = value;
        }
    }

    public float TotalScoreThisRound => _totalScoreThisRound;

    public float BaseDeathPenalty => _baseDeathPenalty;

    public void CalculateFinalScore()
    {
        // Round everything to the nearest integer
        _baseScore = Mathf.RoundToInt(_baseScore);
        _damageDealt = Mathf.RoundToInt(_damageDealt);
        _damageTaken = Mathf.RoundToInt(_damageTaken);
        _dangersDestroyedScore = Mathf.RoundToInt(_dangersDestroyedScore);

        int _deatlhPenaltyScore = Mathf.RoundToInt(_baseDeathPenalty * _timesDead);

        _totalScoreThisRound = Mathf.RoundToInt(_baseScore + _damageDealt - _damageTaken - _deatlhPenaltyScore + _dangersDestroyedScore);
        int previousTotalScore = _gameDataManager.TotalScore;
        _gameDataManager.TotalScore += _totalScoreThisRound;
        _gameDataManager.TotalDestructionScore += Mathf.RoundToInt(_damageDealt + _dangersDestroyedScore);
        _gameDataManager.TotalSafetyScore += Mathf.RoundToInt(_damageTaken + _deatlhPenaltyScore);
        OnMinigameEndScoreCalculated?.Invoke(_gameDataManager.CurrentDay, _deatlhPenaltyScore, previousTotalScore, _gameDataManager.TotalScore);
    }
}
