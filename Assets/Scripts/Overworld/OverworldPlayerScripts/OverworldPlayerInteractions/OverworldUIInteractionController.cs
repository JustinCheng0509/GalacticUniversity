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
    private GameDataManager _gameDataManager;
    private OverworldNPCInteractionController _overworldNPCInteractionController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnStatusChanged += HandlePlayerStatusChanged;
        _overworldInteractionController = FindAnyObjectByType<OverworldInteractionController>();
        _overworldInteractionController.OnInteractableGameObjectChanged += HandleInteractableGameObjectChanged;
        _overworldNPCInteractionController = FindAnyObjectByType<OverworldNPCInteractionController>();

        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnHomeworkProgressUpdated += HandleHomeworkProgressUpdate;
        _gameDataManager.OnNPCRelationshipUpdated += HandleNPCRelationshipUpdate;
    }

    private void HandleInteractableGameObjectChanged(GameObject interactableGameObject)
    {
        if (interactableGameObject != null && !_overworldPlayerStatusController.IsBusy)
        {
            ShowInteractPrompt(GetPromptText(interactableGameObject.tag));
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
                ShowInteractStatus("Homework: " + (int) _gameDataManager.HomeworkProgress + "%");
                break;
            case OverworldPlayerStatus.Working:
                ShowInteractStatus("Working...");
                break;
            case OverworldPlayerStatus.Playing:
                ShowInteractStatus("Playing...");
                break;
            case OverworldPlayerStatus.Chatting:
                ShowInteractStatus("Chatting: " + (int) _gameDataManager.GetNPCRelationship(_overworldNPCInteractionController.CurrentNPC.npcID) + "/100");
                break;
            default:
                HideInteractStatus();
                break;
        }
    }

    private void HandleHomeworkProgressUpdate(float progress)
    {
        if (_overworldPlayerStatusController.CurrentStatus == OverworldPlayerStatus.DoingHomework)
        {
            ShowInteractStatus("Homework: " + (int) progress + "%");
        }
    }

    private void HandleNPCRelationshipUpdate(NPC npc)
    {
        // Debug.Log(_overworldPlayerStatusController.CurrentStatus);
        // Debug.Log(npc.npcID == _overworldNPCInteractionController.CurrentNPC.npcID);
        
        if (_overworldPlayerStatusController.CurrentStatus == OverworldPlayerStatus.Chatting && npc.npcID == _overworldNPCInteractionController.CurrentNPC.npcID)
        {
            ShowInteractStatus("Chatting: " + (int) _gameDataManager.GetNPCRelationship(npc.npcID) + "/100");
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
            case var value when value == GameConstants.INTERACTABLE_TAG_NPC: return "(E) Interact";
            case var value when value == GameConstants.INTERACTABLE_TAG_SHOP: return "(E) Buy food";
            case var value when value == GameConstants.INTERACTABLE_TAG_PLAY: return "(E) Play";
            case var value when value == GameConstants.INTERACTABLE_TAG_WORK: return "(E) Work";
            case var value when value == GameConstants.INTERACTABLE_TAG_CHEST: return "(E) Open chest";
            default: return "";
        }
    }
}
