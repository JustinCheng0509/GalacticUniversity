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

        _minigameCountdownController = FindAnyObjectByType<MinigameCountdownController>();
        _minigameCountdownController.OnCountdownFinished += StartMinigame;

        _tutorialController = FindAnyObjectByType<TutorialController>();
        _tutorialController.OnTutorialCompleted += _minigameCountdownController.StartCountdown;

        _minigameTimerController = FindAnyObjectByType<MinigameTimerController>();
        _minigameTimerController.OnTimerFinished += EndMinigame;

        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        OnMinigameEnd += MinigameEndHandler;
        OnMinigameStart += MinigameStartHandler;
    }

    private void GameDataLoadedHandler()
    {
        
        _switchScene.FadeInScene();   
    }

    private void OnFadeInCompleted()
    {
        if (!_gameDataManager.IsTutorialCompleted(TutorialIDs.TUTORIAL_MINIGAME))
        {
            _tutorialController.ShowTutorial(TutorialIDs.TUTORIAL_MINIGAME);
        } else
        {
            _minigameCountdownController.StartCountdown();
        }
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

    public void ExitMiniGame()
    {
        Time.timeScale = 1; // Ensure time is running normally
        // Set currentTime to 16:00
        _gameDataManager.CurrentTime = GameConstants.CLASS_END_TIME;
        _gameDataManager.CurrentScene = GameConstants.SCENE_OVERWORLD;
        SavedDataManager.SaveGameData(_gameDataManager.GameData);
        _switchScene.FadeOutScene(GameConstants.SCENE_OVERWORLD);
    }
}
