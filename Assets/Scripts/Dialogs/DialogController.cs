using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogPanel;

    [SerializeField] private TMP_Text _dialogNameText;

    [SerializeField] private TMP_Text _dialogText;

    private Dialog _currentDialog;

    public event Action<Dialog> OnDialogEnded;

    private GameDataManager _gameDataManager;

    private OverworldNPCInteractionController _overworldNPCInteractionController;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _overworldNPCInteractionController = FindAnyObjectByType<OverworldNPCInteractionController>();
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
        // Debug.Log(_currentDialog.associatedTutorials.Count);
        _dialogPanel.SetActive(false);
        OnDialogEnded?.Invoke(_currentDialog);
        _currentDialog = null;
    }
    

    public void SetDialog(Dialog dialog)
    {
        _currentDialog = dialog;

        string name = ReplacePlaceholders(dialog.characterName);
        string text = ReplacePlaceholders(dialog.text);

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

    private string ReplacePlaceholders(string text)
    {
        if (string.IsNullOrEmpty(text)) return text;

        text = text.Replace(DialogIDs.DIALOG_PLAYER_NAME_PLACEHOLDER, _gameDataManager.PlayerName);
        
        if (_overworldNPCInteractionController.CurrentNPC != null) {
            text = text.Replace(DialogIDs.DIALOG_CURRENT_NPC_PLACEHOLDER, _overworldNPCInteractionController.CurrentNPC.npcName);
        }

        while(true)
        {
            int startIndex = text.IndexOf(DialogIDs.DIALOG_NPC_NAME_PREFIX);
            if (startIndex == -1) break;

            int endIndex = text.IndexOf("]", startIndex);
            if (endIndex == -1) break;

            string npcPlaceholder = text.Substring(startIndex, endIndex - startIndex + 1);
            string npcId = npcPlaceholder.Substring(1, npcPlaceholder.Length - 2); // Remove the brackets

            // Replace the placeholder with the NPC's name
            string npcName = _gameDataManager.GetNPCName(npcId);
            text = text.Replace(npcPlaceholder, npcName);
        }

        return text;
    }

    // Overloaded function to set dialog with a string ID
    public void SetDialog(string dialogID)
    {
        Dialog dialog = _gameDataManager.DialogList.Find(d => d.dialogID == dialogID);
        if (dialog != null) {
            SetDialog(dialog);
        } else {
            Debug.LogError("Dialog with ID " + dialogID + " not found.");
        }
    }
}
