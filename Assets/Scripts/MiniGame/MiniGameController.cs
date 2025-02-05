using System.Collections;
using TMPro;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 60f; // 1 minute total gameplay

    private float timeRemaining;
    private bool isGameActive = false;

    [SerializeField] private GameObject gameEndPanel;

    [SerializeField] private MiniGameSwitchScene miniGameSwitchScene;

    [SerializeField] private TMP_Text timerText;

    [SerializeField] private TMP_Text countdownText;

    [SerializeField] private PlayerShipInfo playerShipInfo;

    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private GameDataManager gameDataManager;

    [SerializeField] private ScorePanelController scorePanelController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = gameDuration;
        Time.timeScale = 0f;
        playerShipInfo.gameData = gameDataManager.LoadGameData();
        // if it is the first time playing the minigame, show the tutorial
        if (!PlayerPrefs.HasKey("playedMiniGame") && tutorialPanel != null)
        {
            PlayerPrefs.SetInt("playedMiniGame", 1);
            tutorialPanel.SetActive(true);
        } else
        {
            StartCoroutine(CountDownToStartCoroutine());
        }
    }

    public void CountDownToStart()
    {
        StartCoroutine(CountDownToStartCoroutine());
    }

    private IEnumerator CountDownToStartCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        int count = 3;
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
        isGameActive = true;
        playerShipInfo.SpawnPlayerShip();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        // Decrement time remaining
        timeRemaining -= Time.deltaTime;

        // Update the timer UI
        timerText.text = $"{timeRemaining:F1}s";
        
        // Check if total time is up
        if (timeRemaining <= 0)
        {
            PauseGame(); // Pause the game
            scorePanelController.UpdateScorePanel((int) playerShipInfo.score, (int) playerShipInfo.damageDealt, (int) playerShipInfo.dangersDestroyed, (int) playerShipInfo.damageTaken, (int) playerShipInfo.timesDead);
            gameEndPanel.SetActive(true); // Show the game end panel
        }
    }

    // Public methods for game control
    public void PauseGame()
    {
        isGameActive = false;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isGameActive = true;
        Time.timeScale = 1f;
    }

    public void EndGame()
    {
        Time.timeScale = 1; // Ensure time is running normally
        Debug.Log("Time's up! Game Over.");
        // Set currentTime to 16:00
        playerShipInfo.gameData.currentTime = StaticValues.CLASS_END_TIME;

        // Save the game data
        gameDataManager.SaveGameData(playerShipInfo.gameData);

        miniGameSwitchScene.FadeOutGame(StaticValues.SCENE_OVERWORLD);
    }
}
