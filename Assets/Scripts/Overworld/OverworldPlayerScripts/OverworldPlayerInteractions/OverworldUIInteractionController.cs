using TMPro;
using UnityEngine;

public class OverworldUIInteractionController : MonoBehaviour
{
    [SerializeField] private GameObject interactPromptPanel;
    [SerializeField] private TMP_Text interactPromptText;
    [SerializeField] private GameObject interactStatusPanel;
    [SerializeField] private TMP_Text interactStatusText;

    private OverworldPlayerStatusController _overworldPlayerStatusController;
    private OverworldInteractionController _overworldInteractionController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnStatusChanged += HandlePlayerStatusChanged;
        _overworldInteractionController = FindAnyObjectByType<OverworldInteractionController>();
        _overworldInteractionController.OnInteractableTagChanged += HandleInteractableTagChanged;
    }

    private void HandleInteractableTagChanged()
    {
        if (_overworldInteractionController.InteractableTag != "" && !_overworldPlayerStatusController.IsBusy)
        {
            ShowInteractPrompt(GetPromptText(_overworldInteractionController.InteractableTag));
        } else
        {
            HideInteractPrompt();
        }
    }

    private void HandlePlayerStatusChanged(OverworldPlayerStatus status)
    {
        if (_overworldPlayerStatusController.IsBusy)
        {
            HideInteractPrompt();
        }
        switch (status)
        {
            case OverworldPlayerStatus.Sleeping:
                ShowInteractStatus("Sleeping...");
                break;
            case OverworldPlayerStatus.DoingHomework:
                ShowInteractStatus("Doing Homework...");
                break;
            case OverworldPlayerStatus.Working:
                ShowInteractStatus("Working...");
                break;
            case OverworldPlayerStatus.Playing:
                ShowInteractStatus("Playing...");
                break;
            default:
                HideInteractStatus();
                break;
        }
    }

    private void ShowInteractPrompt(string promptText)
    {
        interactPromptPanel.SetActive(true);
        interactPromptText.text = promptText;
    }

    private void HideInteractPrompt()
    {
        interactPromptPanel.SetActive(false);
    }

    private void ShowInteractStatus(string statusText)
    {
        interactStatusPanel.SetActive(true);
        interactStatusText.text = statusText;
    }

    private void HideInteractStatus()
    {
        interactStatusPanel.SetActive(false);
    }

    private string GetPromptText(string tag) {
        // Debug.Log(tag);
        switch (tag) {
            case var value when value == GameConstants.INTERACTABLE_TAG_CLASS: return "(E) Start class";
            case var value when value == GameConstants.INTERACTABLE_TAG_SLEEP: return "(E) Sleep";
            case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK: return "(E) Do homework";
            case var value when value == GameConstants.INTERACTABLE_TAG_NPC: return "(E) Chat";
            case var value when value == GameConstants.INTERACTABLE_TAG_SHOP: return "(E) Buy food";
            case var value when value == GameConstants.INTERACTABLE_TAG_PLAY: return "(E) Play";
            case var value when value == GameConstants.INTERACTABLE_TAG_WORK: return "(E) Work";
            default: return "";
        }
    }
}
