using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum MinigameState
{
    Countdown,
    Playing,
    Paused,
    Ended
}

public class MinigameController : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 60f; // 1 minute total gameplay

    private MinigameState _minigameState = MinigameState.Countdown;

    public MinigameState MinigameState => _minigameState;

    [SerializeField] private GameObject gameEndPanel;
    
    private MinigameScoreController _minigameScoreController;
    private GameDataManager _gameDataManager;
    private TutorialController _tutorialController;
    private MinigameCountdownController _minigameCountdownController;
    private MinigameTimerController _minigameTimerController;
    private SwitchScene _switchScene;

    public event Action OnMinigameStart;
    public event Action OnMinigameEnd;
    public event Action OnMinigamePause;
    public event Action OnMinigameResume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += GameDataLoadedHandler;
        
        _switchScene = FindAnyObjectByType<SwitchScene>();
        _switchScene.OnFadeInCompleted += OnFadeInCompleted;

        _tutorialController = FindAnyObjectByType<TutorialController>();
        _tutorialController.OnTutorialCompleted += _minigameCountdownController.StartCountdown;

        _minigameCountdownController = FindAnyObjectByType<MinigameCountdownController>();
        _minigameCountdownController.OnCountdownFinished += StartMinigame;

        _minigameTimerController = FindAnyObjectByType<MinigameTimerController>();
        _minigameTimerController.OnTimerFinished += EndMinigame;

        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        OnMinigameEnd += MinigameEndHandler;
        OnMinigameStart += MinigameStartHandler;
    }

    private void GameDataLoadedHandler()
    {
        if (!_gameDataManager.IsTutorialCompleted(TutorialIDs.TUTORIAL_MINIGAME))
        {
            _tutorialController.ShowTutorial(TutorialIDs.TUTORIAL_MINIGAME);
        } else
        {
            _switchScene.FadeInScene();
        }
    }

    private void OnFadeInCompleted()
    {
        _minigameCountdownController.StartCountdown();
    }

    private void StartMinigame()
    {
        OnMinigameStart?.Invoke();
    }

    private void EndMinigame()
    {
        OnMinigameEnd?.Invoke();
    }

    private void MinigameStartHandler()
    {
        _minigameState = MinigameState.Playing;
        _minigameTimerController.StartTimer(gameDuration);
    }

    private void MinigameEndHandler()
    {
        _minigameState = MinigameState.Ended;
        Time.timeScale = 0f;
        _minigameScoreController.CalculateFinalScore();
    }

    // Update is called once per frame
    void Update()
    {
        // if (!isGameActive) return;

        // // Decrement time remaining
        // timeRemaining -= Time.deltaTime;

        // // Update the timer UI
        // timerText.text = $"{timeRemaining:F1}s";
        
        // // Check if total time is up
        // if (timeRemaining <= 0)
        // {
        //     PauseGame(); // Pause the game
        //     scorePanelController.UpdateScorePanel((int) playerShipInfo.baseScore, (int) playerShipInfo.damageDealt, (int) playerShipInfo.dangersDestroyed, (int) playerShipInfo.damageTaken, (int) playerShipInfo.timesDead);
        //     gameEndPanel.SetActive(true); // Show the game end panel
        // }
    }

    // Public methods for game control
    public void PauseGame()
    {
        _minigameState = MinigameState.Paused;
        Time.timeScale = 0f;
        OnMinigamePause?.Invoke();
    }

    public void ResumeGame()
    {
        _minigameState = MinigameState.Playing;
        Time.timeScale = 1f;
        OnMinigameResume?.Invoke();
    }

    // public void EndGame()
    // {
    //     Time.timeScale = 1; // Ensure time is running normally
    //     Debug.Log("Time's up! Game Over.");
    //     // Set currentTime to 16:00
    //     playerShipInfo.gameData.currentTime = GameConstants.CLASS_END_TIME;

    //     // Save the game data
    //     gameDataManager.SaveGameData(playerShipInfo.gameData);

    //     miniGameSwitchScene.FadeOutScene(GameConstants.SCENE_OVERWORLD);
    // }
}
