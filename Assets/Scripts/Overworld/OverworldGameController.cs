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
        _switchScene.OnFadeInComplete += HandleFadeInComplete;

        _dialogController = FindAnyObjectByType<DialogController>();
        _dialogController.OnDialogEnd += HandleDialogEnd;
        
        _questController = FindAnyObjectByType<QuestController>();
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

    // private void HandleIntroDialogEnd()
    // {
    //     _dialogController.OnDialogEnd -= HandleIntroDialogEnd;
    //     _questController.AddQuest(QuestIDs.QUEST_CLASSROOM_INTRO);
    //     _questController.AddQuest(QuestIDs.QUEST_DORM_INTRO);
    //     _questController.AddQuest(QuestIDs.QUEST_SHOP_INTRO);
    //     _questController.AddQuest(QuestIDs.QUEST_WORK_INTRO);
    //     _questController.AddQuest(QuestIDs.QUEST_PLAY_ROOM_INTRO);
    //     _tutorialController.ShowTutorial(TutorialIDs.TUTORIAL_START);
    // }
}
