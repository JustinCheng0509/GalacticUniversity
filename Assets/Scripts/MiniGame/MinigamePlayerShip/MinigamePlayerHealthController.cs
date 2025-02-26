using System;
using UnityEngine;

public class MinigamePlayerHealthController : MonoBehaviour
{
    [SerializeField] private GameObject _playerShip;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private Transform _playerShipSpawnPoint;

    private GameDataManager _gameDataManager;
    private MinigameScoreController _minigameScoreController;

    private float _maxHealth = 100f;
    private float _currentHealth = 100f;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action OnHealthChanged;
    public event Action OnPlayerDeath;
    public event Action OnPlayerRespawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
    }

    public void TakeDamage(float damage)
    {
        if (GameConstants.USE_SKILL_SYSTEM)
{
            damage *= Mathf.Lerp(1f, 1f / 3f, _gameDataManager.Mechanics / 100f);
        }

        if (damage >= _currentHealth)
        {
            damage = _currentHealth;
        }

        _minigameScoreController.DamageTaken += damage;
        _minigameScoreController.Score -= damage;

        _currentHealth = Math.Max(0, _currentHealth - damage);
        OnHealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            DestroyPlayerShip();
        }
    }

    public void DestroyPlayerShip()
    {
        _minigameScoreController.TimesDead++;
        _minigameScoreController.Score -= _minigameScoreController.BaseDeathPenalty;

        _playerShip.SetActive(false);
        OnPlayerDeath?.Invoke();

        Instantiate(_explosionEffect, _playerShip.transform.position, Quaternion.identity);
        // Respawn the player ship after 2 seconds
        Invoke(nameof(SpawnPlayerShip), 2f);
    }

    public void SpawnPlayerShip()
    {
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke();
        _playerShip.transform.position = _playerShipSpawnPoint.position;
        _playerShip.SetActive(true);
        OnPlayerRespawn?.Invoke();
    }
}
