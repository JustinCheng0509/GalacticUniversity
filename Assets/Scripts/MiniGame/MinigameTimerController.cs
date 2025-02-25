using System;
using TMPro;
using UnityEngine;

public class MinigameTimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float timeRemaining = 0f;             // Timer for the minigame
    private bool isTimerRunning = false;          // Flag to check if timer is running

    public event Action OnTimerFinished; // Event to notify when the timer finishes

    public void StartTimer(float duration)
    {
        timeRemaining = duration;
        isTimerRunning = true;
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timeRemaining = 0f;
        timerText.text = "0.0s";  // Display "0.0s" when stopped
        OnTimerFinished?.Invoke(); // Notify that the timer has finished
    }

    public void Update()
    {
        if (!isTimerRunning) return;

        timeRemaining -= Time.deltaTime;
        timerText.text = $"{timeRemaining:F1}s";

        if (timeRemaining <= 0)
        {
            // Timer is done, handle game end logic here if needed
            StopTimer();
        }
    }
}
