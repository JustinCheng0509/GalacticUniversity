using UnityEngine;

public class OverworldGameController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private SwitchScene _switchScene;

    private DialogController _dialogController;
    private QuestController _questController;
    private TutorialController _tutorialController;
    
    private OverworldUILayoutController _overworldUILayoutController;

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
        Debug.Log(dialog.text);
        Debug.Log(dialog.associatedTutorials.Count);
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
}
