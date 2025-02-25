using UnityEngine;
using UnityEngine.UI;

public class MinigameUIPlayerHealth : MonoBehaviour
{
    private RectTransform healthBarRect;

    private float originalWidth;

    private MinigamePlayerHealthController _playerHealth;

    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();
        originalWidth = healthBarRect.sizeDelta.x;
        _playerHealth = FindAnyObjectByType<MinigamePlayerHealthController>();
        _playerHealth.OnHealthChanged += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        float healthPercent = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
        healthBarRect.sizeDelta = new Vector2(originalWidth * healthPercent, healthBarRect.sizeDelta.y);
    }
}