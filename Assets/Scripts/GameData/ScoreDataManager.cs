using System.Collections.Generic;
using UnityEngine;

public class ScoreDataManager
{
  private int _totalScore = 0;
  private int _totalDamageDealt = 0;
  private int _dangersDestroyedScore = 0;
  private int _totalDamageTaken = 0;
  private int _timesDead = 0;
  private List<LeaderboardEntry> _leaderboard = new List<LeaderboardEntry>();

  public int TotalScore
  {
    get => _totalScore;
    set => _totalScore = value;
  }
  public int TotalDamageDealt
  {
    get => _totalDamageDealt;
    set => _totalDamageDealt = value;
  }
  public int DangersDestroyedScore
  {
    get => _dangersDestroyedScore;
    set => _dangersDestroyedScore = value;
  }
  public int TotalDamageTaken
  {
    get => _totalDamageTaken;
    set => _totalDamageTaken = value;
  }
  public int TimesDead
  {
    get => _timesDead;
    set => _timesDead = value;
  }
  public List<LeaderboardEntry> Leaderboard
  {
    get => _leaderboard;
    set => _leaderboard = value;
  }

  public void Initialize(int totalScore, int totalDamageDealt, int dangersDestroyedScore, int totalDamageTaken, int timesDead, List<LeaderboardEntry> leaderboard)
  {
    _totalScore = totalScore;
    _totalDamageDealt = totalDamageDealt;
    _dangersDestroyedScore = dangersDestroyedScore;
    _totalDamageTaken = totalDamageTaken;
    _timesDead = timesDead;
    _leaderboard = leaderboard;
  }

  public void GenerateLeaderboardScore(string playerName, int currentDay, float mechanics, float destruction, float maneuverability)
  {
    for (int i = 0; i < _leaderboard.Count; i++)
    {
      if (_leaderboard[i].name == playerName)
      {
        _leaderboard[i].totalScore = _totalScore;
      } else {
        int npcDestructionScore = (int) (GetExpectedDestructionScore(currentDay, destruction) * UnityEngine.Random.Range(MinigameConstants.MINIGAME_NPC_MIN_RANGE, MinigameConstants.MINIGAME_NPC_MAX_RANGE));
        int npcSafetyScore = (int) (GetExpectedSafetyScore(currentDay, mechanics, maneuverability) * UnityEngine.Random.Range(MinigameConstants.MINIGAME_NPC_MIN_RANGE, MinigameConstants.MINIGAME_NPC_MAX_RANGE));
        int npcTotalScore = (int) (npcDestructionScore - npcSafetyScore);
          
        _leaderboard[i].totalScore += npcTotalScore;
      }
    }
  }

  public List<LeaderboardEntry> GetSortedLeaderboard(bool ascending = false)
  {
    if (ascending)
    {
      _leaderboard.Sort((x, y) => x.totalScore.CompareTo(y.totalScore));
    }
    else
    {
      _leaderboard.Sort((x, y) => y.totalScore.CompareTo(x.totalScore));
    }
    return _leaderboard;
  }
  
  private int GetExpectedDestructionScore(int currentDay, float destruction)
  {
    float spawnDelay = MinigameConstants.MINIGAME_ENEMY_BASE_SPAWN_DELAY - MinigameConstants.MINIGAME_ENEMY_DAILY_SPAWN_DELAY_DECREASE * (currentDay - 1);
    float maxScale = MinigameConstants.MINIGAME_ENEMY_BASE_MAX_SCALE + MinigameConstants.MINIGAME_ENEMY_DAILY_MAX_SCALE_INCREASE * (currentDay - 1);
    float scorePerAsteroidDestroyed = MinigameConstants.MINIGAME_ENEMY_BASE_SCORE * (maxScale + 1);

    // Total asteroids in 60s
    int totalAsteroids = Mathf.RoundToInt(60f / spawnDelay);

    // Expected asteroid destruction rate (estimate: 10% to 50% destruction depending on skill)
    float destructionRate = Mathf.Lerp(0.1f, 0.5f, destruction / 100f);
    int expectedAsteroidsDestroyed = Mathf.RoundToInt(totalAsteroids * destructionRate);

    return Mathf.RoundToInt(expectedAsteroidsDestroyed * scorePerAsteroidDestroyed);
  }

  private int GetExpectedSafetyScore(int currentDay, float mechanics, float maneuverability)
  {
    float spawnDelay = MinigameConstants.MINIGAME_ENEMY_BASE_SPAWN_DELAY - MinigameConstants.MINIGAME_ENEMY_DAILY_SPAWN_DELAY_DECREASE * (currentDay - 1);
    float maxScale = MinigameConstants.MINIGAME_ENEMY_BASE_MAX_SCALE + MinigameConstants.MINIGAME_ENEMY_DAILY_MAX_SCALE_INCREASE * (currentDay - 1);
    float asteroidDamage = MinigameConstants.MINIGAME_ENEMY_BASE_DAMAGE * maxScale;

    // Player skill multipliers
    float mechanicsMultiplier = Mathf.Lerp(1f, 1f / 3f, mechanics / 100f);

    // Total asteroids in 60s
    int totalAsteroids = Mathf.RoundToInt(60f / spawnDelay);

    // Expected damage taken (assuming 10-40% of asteroids hit the player)
    float hitRate = Mathf.Lerp(0.1f, 0.4f, (100 - maneuverability) / 100f);
    float expectedDamageTaken = totalAsteroids * hitRate * asteroidDamage * mechanicsMultiplier;
    int expectedDamageTakenScore = Mathf.RoundToInt(expectedDamageTaken);

    // Player is expected to die less when they have higher mechanics skill
    int totalDeaths = Mathf.CeilToInt(Mathf.Lerp(3f, 1f, Mathf.Clamp01((currentDay - 1) / 9f)));
    int expectedDeathPenalty = Mathf.RoundToInt(MinigameConstants.MINIGAME_PLAYER_BASE_DEATH_PENALTY * totalDeaths);

    return Mathf.RoundToInt(expectedDamageTakenScore + expectedDeathPenalty);
  }
}