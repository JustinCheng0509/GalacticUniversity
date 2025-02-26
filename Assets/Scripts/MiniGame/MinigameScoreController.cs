using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinigameScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private float _score = 0f;
    private float _baseScore = 3000f;
    private float _damageDealt = 0f;
    private float _damageTaken = 0f;
    private float _dangersDestroyedScore = 0f;
    private int _timesDead = 0;
    private float _baseDeathPenalty = 500f;


    private int _totalScoreThisRound = 0;

    public event Action<int, int, int, int> OnMinigameEndScoreCalculated;
    public event Action<List<LeaderboardEntry>, string> OnLeaderboardUpdated;

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
            _scoreText.text = "Score: " + Mathf.RoundToInt(_score).ToString();
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
        GenerateLeaderboardScore();
        OnMinigameEndScoreCalculated?.Invoke(_gameDataManager.CurrentDay, _deatlhPenaltyScore, previousTotalScore, _gameDataManager.TotalScore);
    }

    private void GenerateLeaderboardScore()
    {
        List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>(_gameDataManager.Leaderboard);
        for (int i = 0; i < leaderboard.Count; i++)
        {
            if (leaderboard[i].name == _gameDataManager.PlayerName)
            {
                leaderboard[i].totalScore = _gameDataManager.TotalScore;
                leaderboard[i].destructionScore = _gameDataManager.TotalDestructionScore;
                leaderboard[i].safetyScore = _gameDataManager.TotalSafetyScore;
            } else {
                float minRange = 0.5f;
                float maxRange = 1.5f;
                int npcDestructionScore = (int) (GetExpectedDestructionScore() * UnityEngine.Random.Range(minRange, maxRange));
                int npcSafetyScore = (int) (GetExpectedSafetyScore() * UnityEngine.Random.Range(minRange, maxRange));
                int npcTotalScore = (int) (_baseScore + npcDestructionScore - npcSafetyScore);
                
                leaderboard[i].totalScore += npcTotalScore;
                leaderboard[i].destructionScore += npcDestructionScore;
                leaderboard[i].safetyScore += npcSafetyScore;
            }
        }

        _gameDataManager.Leaderboard = leaderboard;
        OnLeaderboardUpdated?.Invoke(leaderboard, _gameDataManager.PlayerName);
    }

    private int GetExpectedDestructionScore()
    {
        int day = _gameDataManager.CurrentDay;
        float spawnRate = 0.4f - 0.05f * (day - 1);
        float maxScale = 2f + 0.4f * (day - 1);
        float scorePerAsteroidDestroyed = 100 * (maxScale + 1);

        // Total asteroids in 60s
        int totalAsteroids = Mathf.RoundToInt(60f / spawnRate);

        // Expected asteroid destruction rate (estimate: 10% to 50% destruction depending on skill)
        float destructionRate = Mathf.Lerp(0.1f, 0.5f, _gameDataManager.Destruction / 100f);
        int expectedAsteroidsDestroyed = Mathf.RoundToInt(totalAsteroids * destructionRate);

        return Mathf.RoundToInt(expectedAsteroidsDestroyed * scorePerAsteroidDestroyed);
    }

    private int GetExpectedSafetyScore()
    {
        int day = _gameDataManager.CurrentDay;
        float spawnRate = 0.4f - 0.05f * (day - 1);
        float maxScale = 2f + 0.4f * (day - 1);
        float asteroidDamage = 20 * maxScale;

        // Player skill multipliers
        float mechanicsMultiplier = Mathf.Lerp(1f, 1f / 3f, _gameDataManager.Mechanics / 100f);

        // Total asteroids in 60s
        int totalAsteroids = Mathf.RoundToInt(60f / spawnRate);

        // Expected damage taken (assuming 10-40% of asteroids hit the player)
        float hitRate = Mathf.Lerp(0.1f, 0.4f, (100 - _gameDataManager.Maneuverability) / 100f);
        float expectedDamageTaken = totalAsteroids * hitRate * asteroidDamage * mechanicsMultiplier;
        int expectedDamageTakenScore = Mathf.RoundToInt(expectedDamageTaken);

        // Player is expected to die less when they have higher mechanics skill
        int totalDeaths = Mathf.CeilToInt(Mathf.Lerp(3f, 1f, Mathf.Clamp01((day - 1) / 9f)));
        int expectedDeathPenalty = Mathf.RoundToInt(_baseDeathPenalty * totalDeaths);

        return Mathf.RoundToInt(expectedDamageTakenScore + expectedDeathPenalty);
    }
}
