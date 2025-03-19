using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MinigameCountdownController : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private float countdownTime = 3f; // Countdown time in seconds
    
    public event Action OnCountdownFinished;

    public void StartCountdown()
    {
        StartCoroutine(CountDownToStartCoroutine());
    }

    private IEnumerator CountDownToStartCoroutine()
    {
        Time.timeScale = 0f;
        countdownText.gameObject.SetActive(true);
        int count = countdownTime > 0 ? Mathf.CeilToInt(countdownTime) : 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f;
        OnCountdownFinished?.Invoke();
    }
}
