using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldInteractionController : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactAction;

    private GameObject _interactableGameObject;

    public GameObject InteractableGameObject { 
        get => _interactableGameObject;
        set {
            _interactableGameObject = value;
            OnInteractableGameObjectChanged?.Invoke(_interactableGameObject);
        }
    }

    public event Action<GameObject> OnInteractableGameObjectChanged;

    private OverworldPlayerStatusController _overworldPlayerStatusController;
    private GameDataManager _gameDataManager;
    private OverworldTimeController _overworldTimeController;
    private DialogController _dialogController;
    private SwitchScene _switchScene;
    private OverworldNPCInteractionController _overworldNPCInteractionController;
    private OverworldUIChestController _overworldUIChestController;

    [SerializeField] private AudioSource _interactionAudioSource;
    [SerializeField] private AudioClip _sfxSchoolBellClip;


    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _dialogController = FindAnyObjectByType<DialogController>();
        _switchScene = FindAnyObjectByType<SwitchScene>();
        _overworldNPCInteractionController = FindAnyObjectByType<OverworldNPCInteractionController>();
        _overworldUIChestController = FindAnyObjectByType<OverworldUIChestController>();
    }

    private void OnEnable()
    {
        _interactAction.action.Enable();
        _interactAction.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _interactAction.action.performed -= OnInteractPerformed;
        _interactAction.action.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (_overworldPlayerStatusController.IsBusy)
        {
            _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Idle;
        }
        else
        {
            if (InteractableGameObject != null)
            {
                StartInteraction(InteractableGameObject);
            }
        }
    }
    
    public void StartInteraction(GameObject interactableGameObject) {
        switch (interactableGameObject.tag) {
            case var value when value == GameConstants.INTERACTABLE_TAG_CLASS: StartClass(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_SLEEP: StartSleep(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_NPC: StartNPCInteraction(interactableGameObject); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK: StartHomework(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_WORK: StartWork(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_PLAY: StartPlay(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_SHOP: StartShop(); break;
            case var value when value == GameConstants.INTERACTABLE_TAG_CHEST: OpenChest(interactableGameObject); break;
            default: Debug.Log("No interaction found"); break;
        }
    }

    private void OpenChest(GameObject interactableGameObject) {
        OverworldChestPrefabController chestController = interactableGameObject.GetComponent<OverworldChestPrefabController>();
        if (chestController == null) {
            Debug.LogWarning("ChestController not found in Chest object.");
            return;
        }
        Chest Chest = chestController.Chest;
        _gameDataManager.OpenChest(Chest);
        _overworldUIChestController.OpenChestUI(Chest);
    }

    private void StartNPCInteraction(GameObject interactableGameObject) {
        NPCPrefabController npcPrefabController = interactableGameObject.GetComponent<NPCPrefabController>();
        if (npcPrefabController == null) {
            Debug.LogWarning("NPCController not found in NPC object.");
            return;
        }
        _overworldNPCInteractionController.StartNPCInteraction(npcPrefabController.Npc);
    }

    private void StartClass() {
        if (_gameDataManager.Attendance == AttendanceStatus.ATTENDED) {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_ALREADY_ATTENDED);
            return;
        }

        if (!_overworldTimeController.CanAttendClass) {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_NOT_CLASS_TIME);
            return;
        }

        _gameDataManager.Attendance = AttendanceStatus.ATTENDED;
        SavedDataManager.SaveGameData(_gameDataManager.GameData);
        // Play the school bell sound
        _interactionAudioSource.PlayOneShot(_sfxSchoolBellClip);
        // Open the mini-game scene
        _switchScene.FadeOutScene(GameConstants.SCENE_MINIGAME, 3f);
    }

    private void StartSleep() {
        _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Sleeping;
    }

    private void StartHomework() {
        if (_gameDataManager.HomeworkProgress >= 100)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_HOMEWORK_DONE);
            return;
        }
        if (_gameDataManager.Hunger < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_HUNGRY);
            return;
        }
        if (_gameDataManager.Energy < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_TIRED);
            return;
        }
        if (_gameDataManager.Mood < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_STRESSED);
            return;
        }
        // Set the player to do homework
        _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.DoingHomework;
    }

    private void StartWork() {
        if (_gameDataManager.Hunger < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_HUNGRY);
            return;
        }
        if (_gameDataManager.Energy < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_TIRED);
            return;
        }
        if (_gameDataManager.Mood < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_TOO_STRESSED);
            return;
        }
        // Set the player to work
        _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Working;
    }

    private void StartPlay() {
        // Set the player to play
        _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Playing;
    }

    private void StartShop() {
        // If player does not have enough money, show dialog
        if (_gameDataManager.Money < 20)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_NOT_ENOUGH_MONEY);
            return;
        }
        if (_gameDataManager.Hunger > 90)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_STATUS_ALREADY_FULL);
            return;
        }
        _gameDataManager.Money -= 20;
        _gameDataManager.Hunger += 30;
    }
}
