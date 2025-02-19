using System;
using UnityEngine;

public class MinigamePlayerHealthController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private float _maxHealth = 100f;
    private float _currentHealth = 100f;
    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;

    public event Action OnHealthChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
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
    }
}
