using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverworldInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject interactPanel;
    
    public InputActionReference interact;

    private string interactableTag = "";

    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private AudioSource schoolBell;

    void Update() {
        
        if (interact.action.triggered) {
            if (playerInfo.IsBusy()) {
                playerInfo.CancelActions();
            } else if (interactableTag != "") {
                StartInteraction(interactableTag);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if the layer of the object is Interactable
        if (other.gameObject.layer == LayerMask.NameToLayer(CustomString.INTERACTABLE_LAYER) && !playerInfo.IsBusy()) {  
            interactPanel.SetActive(true);
            interactableTag = other.gameObject.tag;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(CustomString.INTERACTABLE_LAYER) && !playerInfo.IsBusy()) {
            interactPanel.SetActive(true);
            interactableTag = other.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(CustomString.INTERACTABLE_LAYER)) {
            interactPanel.SetActive(false);
            interactableTag = "";
        }
    }

    private void StartInteraction(string tag) {
        switch (tag) {
            case var value when value == CustomString.INTERACTABLE_TAG_CLASS:
                Debug.Log("Interacting with class");
                StartClass();
                break;
            case var value when value == CustomString.INTERACTABLE_TAG_SLEEP:
                Debug.Log("Interacting with sleep");
                break;
            case var value when value == CustomString.INTERACTABLE_TAG_HOMEWORK:
                Debug.Log("Interacting with homework");
                break;
            default:
                Debug.Log("No interaction found");
                break;
        }
    }

    private void StartClass()
    {
        // Play the school bell sound
        schoolBell.Play();
        // Open the mini-game scene
        SceneManager.LoadScene(CustomString.SCENE_MINIGAME);
    }
}
