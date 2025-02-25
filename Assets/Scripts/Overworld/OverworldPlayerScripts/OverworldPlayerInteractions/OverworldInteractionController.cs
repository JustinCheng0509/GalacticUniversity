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

    [SerializeField] private AudioSource _interactionAudioSource;
    [SerializeField] private AudioClip _sfxSchoolBellClip;


    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _overworldTimeController = FindAnyObjectByType<OverworldTimeController>();
        _dialogController = FindAnyObjectByType<DialogController>();
        _switchScene = FindAnyObjectByType<SwitchScene>();
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
            case var value when value == GameConstants.INTERACTABLE_TAG_NPC: StartNPCInteract(interactableGameObject); break;
            // case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK: StartHomework(); break;
            // case var value when value == GameConstants.INTERACTABLE_TAG_WORK: StartWork(); break;
            // case var value when value == GameConstants.INTERACTABLE_TAG_PLAY: StartPlay(); break;
            // case var value when value == GameConstants.INTERACTABLE_TAG_SHOP: StartShop(); break;
            default: Debug.Log("No interaction found"); break;
        }
    }

    private void StartNPCInteract(GameObject interactableGameObject) {
        NPCController npcController = interactableGameObject.GetComponent<NPCController>();
        if (npcController == null) {
            Debug.LogWarning("NPCController not found in NPC object.");
            return;
        }
        npcController.Interact();
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
}
