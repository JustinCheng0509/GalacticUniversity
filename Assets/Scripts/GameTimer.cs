using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 90f; // Total time limit in seconds
    private float elapsedTime = 0f; // Tracks elapsed time
    public Text timerText; // Optional: Assign a UI Text component to display the timer
    
    public GameObject hiddenBoss; // Reference to the hidden boss GameObject
    private bool isBossFight = false; // Tracks if boss fight is active
    private bool isStopped = false; // Tracks whether the game is stopped
    private const float BOSS_ACTIVATION_TIME = 30f; // Boss appears in last 30 seconds

    void Start()
    {
        // Ensure boss is initially inactive
        if (hiddenBoss != null)
        {
            hiddenBoss.SetActive(false);
        }
    }

    void Update()
    {
        // If the game is stopped, do nothing
        if (isStopped)
            return;

        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate remaining time
        float timeRemaining = Mathf.Max(playTimeLimit - elapsedTime, 0);

        // Update the timer UI (if assigned)
        if (timerText != null)
        {
            if (!isBossFight)
            {
                timerText.text = $"Time Remaining: {timeRemaining:F1}s";
            }
            else
            {
                timerText.text = $"Boss Fight: {timeRemaining:F1}s";
            }
        }

        // Check if it's time to activate the boss (when 30 seconds remain)
        if (!isBossFight && timeRemaining <= BOSS_ACTIVATION_TIME)
        {
            ActivateBoss();
        }

        // Check if total time is up
        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    void ActivateBoss()
    {
        isBossFight = true;
        if (hiddenBoss != null)
        {
            hiddenBoss.SetActive(true);
            Debug.Log("Boss Fight Activated! You have 30 seconds to defeat the boss!");
        }
        else
        {
            Debug.LogWarning("Hidden Boss GameObject not assigned!");
        }
    }

    public void StopGame()
    {
        isStopped = true;
        Time.timeScale = 0; // Freeze all time-based actions
        Debug.Log("Game Stopped!");
    }

    public void ResumeGame()
    {
        isStopped = false;
        Time.timeScale = 1; // Resume all time-based actions
        Debug.Log("Game Resumed!");
    }

    void EndGame()
    {
        Debug.Log("Time's up! Game Over.");
        SceneManager.LoadScene(SceneName.SCENE_OVERWORLD); // Example: Reload the scene
    }

    // Call this method when the boss is defeated
    public void OnBossDefeated()
    {
        Debug.Log("Congratulations! Boss Defeated!");
        // Add your win condition handling here
        // For example: Load a victory scene, show victory UI, etc.
    }
}