using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float playTimeLimit = 90f; // Time limit in seconds
    private float elapsedTime = 0f; // Tracks elapsed time
    public Text timerText; // Optional: Assign a UI Text component to display the timer

    private bool isStopped = false; // Tracks whether the game is stopped

    void Update()
    {
        // If the game is stopped, do nothing
        if (isStopped)
            return;

        // Increment elapsed time
        elapsedTime += Time.deltaTime;

        // Update the timer UI (if assigned)
        if (timerText != null)
        {
            float timeRemaining = Mathf.Max(playTimeLimit - elapsedTime, 0);
            timerText.text = $"Time: {timeRemaining:F1}s";
        }

        // Check if time limit is reached
        if (elapsedTime >= playTimeLimit)
        {
            EndGame();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Example: Reload the scene
    }
}
