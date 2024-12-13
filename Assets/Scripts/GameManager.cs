using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 180f; // 3 minutes total gameplay
    [SerializeField] private float bossSpawnTime = 30f; // Time remaining when boss spawns
    private float currentTime;
    private bool isGameActive = true;
    private bool bossSpawned = false;

    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Vector3 bossSpawnPosition = new Vector3(0, 5, 0);

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject warningText;
    [SerializeField] private GameObject gameOverPanel;

    // Events for game state changes
    public event Action<float> OnTimeChanged;
    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameOver;
    public event Action OnBossSpawn;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        currentTime = gameDuration;
        isGameActive = true;
        bossSpawned = false;

        // Initialize UI elements
        if (timerText != null) UpdateTimerDisplay();
        if (warningText != null) warningText.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (bossPrefab != null) bossPrefab.SetActive(false);

        // Notify game start
        OnGameStart?.Invoke();
    }

    void Update()
    {
        if (!isGameActive) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            OnTimeChanged?.Invoke(currentTime);
            UpdateTimerDisplay();

            // Boss spawn logic
            if (currentTime <= bossSpawnTime && !bossSpawned)
            {
                ShowBossWarning();
                SpawnBoss();
            }
        }
        else
        {
            HandleGameOver();
        }
    }

    private void ShowBossWarning()
    {
        if (warningText != null && currentTime <= (bossSpawnTime + 2f) && currentTime > bossSpawnTime)
        {
            warningText.SetActive(true);
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            bossPrefab.transform.position = bossSpawnPosition;
            bossPrefab.SetActive(true);
            bossSpawned = true;

            if (warningText != null)
            {
                warningText.SetActive(false);
            }

            OnBossSpawn?.Invoke();
        }
        else
        {
            Debug.LogError("Boss prefab not assigned in GameManager!");
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void HandleGameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        currentTime = 0;
        UpdateTimerDisplay();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        OnGameOver?.Invoke();
    }

    // Public methods for game control
    public void PauseGame()
    {
        if (isGameActive)
        {
            isGameActive = false;
            Time.timeScale = 0f;
            OnGamePause?.Invoke();
        }
    }

    public void ResumeGame()
    {
        if (!isGameActive)
        {
            isGameActive = true;
            Time.timeScale = 1f;
            OnGameResume?.Invoke();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        InitializeGame();
    }

    // Utility methods
    public float GetRemainingTime()
    {
        return currentTime;
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public float GetBossSpawnTime()
    {
        return bossSpawnTime;
    }

    public bool IsBossSpawned()
    {
        return bossSpawned;
    }
}