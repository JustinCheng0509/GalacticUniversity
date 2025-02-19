using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldInteractionController : MonoBehaviour
{
    [SerializeField] private InputActionReference _interactAction;

    private string _interactableTag = "";

    public string InteractableTag { 
        get => _interactableTag;
        set {
            _interactableTag = value;
            OnInteractableTagChanged?.Invoke();
        }
    }

    public event Action OnInteractableTagChanged;

    private OverworldPlayerStatusController _overworldPlayerStatusController;

    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
    }

    void FixedUpdate()
    {
        if (_interactAction.action.triggered)
        {
            if (_overworldPlayerStatusController.IsBusy)
            {
                _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Idle;
            } else
            {
                // Handle interaction logic here
                if (_interactableTag != "")
                {
                    StartInteraction(_interactableTag);
                }
            }
        }
    }
    
    public void StartInteraction(string tag) {
        // switch (tag) {
        //     case var value when value == GameConstants.INTERACTABLE_TAG_CLASS: StartClass(); break;
        //     case var value when value == GameConstants.INTERACTABLE_TAG_SLEEP: StartSleep(); break;
        //     case var value when value == GameConstants.INTERACTABLE_TAG_HOMEWORK: StartHomework(); break;
        //     case var value when value == GameConstants.INTERACTABLE_TAG_WORK: StartWork(); break;
        //     case var value when value == GameConstants.INTERACTABLE_TAG_PLAY: StartPlay(); break;
        //     // case var value when value == GameConstants.INTERACTABLE_TAG_NPC: StartChat(); break;
        //     // case var value when value == GameConstants.INTERACTABLE_TAG_SHOP: StartShop(); break;
        //     default: Debug.Log("No interaction found"); break;
        // }
    }
}
