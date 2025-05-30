using UnityEngine;

public class OverworldGameController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private SwitchScene _switchScene;
    private DialogController _dialogController;
    private QuestController _questController;
    private TutorialController _tutorialController;
    private OverworldUILayoutController _overworldUILayoutController;
    private OverworldTimeController _overworldTimeController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;

        _switchScene = FindAnyObjectByType<SwitchScene>();
        _switchScene.OnFadeInCompleted += HandleFadeInComplete;

        _dialogController = FindAnyObjectByType<DialogController>();
        _dialogController.OnDialogEnded += HandleDialogEnd;
        
        _questController = FindAnyObjectByType<QuestController>();
        _questController.OnQuestCompleted += HandleQuestComplete;

        _tutorialController = FindAnyObjectByType<TutorialController>();
        _overworldUILayoutController = FindAnyObjectByType<OverworldUILayoutController>();

        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _overworldTimeController.OnLastDayEnded += HandleLastDayEnded;
    }

    private void HandleLastDayEnded()
    {
        _switchScene.FadeOutScene(GameConstants.SCENE_ENDING, 1f);
    }

    private void OnGameDataLoaded()
    {
        _overworldUILayoutController.ForceLayoutRebuild();
        _switchScene.FadeInScene();
    }

    private void HandleFadeInComplete()
    {
        if (_gameDataManager.CurrentDay == 1 && !_gameDataManager.IntroDialogPlayed)
        {
            _gameDataManager.IntroDialogPlayed = true;
            _dialogController.SetDialog(DialogIDs.DIALOG_INTRO);
        }
        PauseManager.Instance.CheckPauseState();
    }

    private void HandleDialogEnd(Dialog dialog)
    {
        if (dialog.associatedTutorials != null) {
            _tutorialController.ShowTutorial(dialog.associatedTutorials);
        }
        if (dialog.associatedQuests != null) {
            _questController.AddQuests(dialog.associatedQuests);
        }
    }

    private void HandleQuestComplete(Quest quest)
    {
        if (quest.completeDialog != null) {
            _dialogController.SetDialog(quest.completeDialog);
        }
    }

    public void SaveAndQuit()
    {
        SavedDataManager.SaveGameData(_gameDataManager.GameData);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void SaveAndBackToMenu()
    {
        SavedDataManager.SaveGameData(_gameDataManager.GameData);
        _switchScene.FadeOutScene(GameConstants.SCENE_MAIN_MENU, 1f);
    }
}
