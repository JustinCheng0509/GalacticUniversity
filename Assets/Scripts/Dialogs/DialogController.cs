using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    private List<Dialog> _dialogList = new List<Dialog>();

    [SerializeField] private GameObject _dialogPanel;

    [SerializeField] private TMP_Text _dialogNameText;

    [SerializeField] private TMP_Text _dialogText;

    private Dialog _currentDialog;

    public event Action OnDialogEnd;

    private GameDataManager _gameDataManager;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        // Load all dialogs from resources
        _dialogList.AddRange(Resources.LoadAll<Dialog>("Dialogs"));
    }

    public void AdvanceDialog()
    {
        if (_currentDialog.nextDialog != null) {
            SetDialog(_currentDialog.nextDialog);
        } else {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        _dialogPanel.SetActive(false);
        Time.timeScale = 1;
        OnDialogEnd?.Invoke();
        // if (CompareDialogs(currentDialogs, introDialogs)) {
        //     tutorialController.ShowTutorial(tutorialController.startTutorial);
        // } else if (CompareDialogs(currentDialogs, classroomTriggerDialogs)) {
        //     tutorialController.ShowTutorial(tutorialController.classTutorial);
        // } else if (CompareDialogs(currentDialogs, shopTriggerDialogs)) {
        //     tutorialController.ShowTutorial(tutorialController.shopTutorial);
        // } else if (CompareDialogs(currentDialogs, dormTriggerDialogs)) {
        //     tutorialController.ShowTutorial(tutorialController.homeworkTutorial);
        // } else if (CompareDialogs(currentDialogs, workTriggerDialogs)) {
        //     tutorialController.ShowTutorial(tutorialController.workTutorial);
        // } else if (CompareDialogs(currentDialogs, playRoomTriggerDialogs) && PlayerPrefs.GetInt("NeedTutorial", 0) == 0) {
        //     tutorialController.ShowTutorial(tutorialController.needTutorial);
        // }
    }
    

    public void SetDialog(Dialog dialog)
    {
        Time.timeScale = 0;
        _currentDialog = dialog;

        string name = dialog.characterName;
        string text = dialog.text;

        // If dialog contains PLAYER_NAME_PLACEHOLDER, replace it with the player's name
        if (name.Contains(GameConstants.PLAYER_NAME_PLACEHOLDER)) {
            name = name.Replace(GameConstants.PLAYER_NAME_PLACEHOLDER, _gameDataManager.PlayerName);
        }
        if (text.Contains(GameConstants.PLAYER_NAME_PLACEHOLDER)) {
            text = text.Replace(GameConstants.PLAYER_NAME_PLACEHOLDER, _gameDataManager.PlayerName);
        }

        _dialogNameText.text = name;
        _dialogText.text = text;

        if (dialog.isSelfDialog) {
            // italicize the text
            _dialogText.fontStyle = FontStyles.Italic;
        } else {
            // remove italicize
            _dialogText.fontStyle = FontStyles.Normal;
        }

        _dialogNameText.alignment = dialog.isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
        _dialogText.alignment = dialog.isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
        
        _dialogPanel.SetActive(true);
    }

    // Overloaded function to set dialog with a string ID
    public void SetDialog(string dialogID)
    {
        Dialog dialog = _dialogList.Find(d => d.dialogID == dialogID);
        if (dialog != null) {
            SetDialog(dialog);
        } else {
            Debug.LogError("Dialog with ID " + dialogID + " not found.");
        }
    }
}
