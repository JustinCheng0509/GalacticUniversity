using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OverworldInteract : MonoBehaviour
{
    [SerializeField]
    private GameObject interactPanel;
    
    public InputActionReference interact;

    private string interactableTag = "";

    void Update() {
        if (interact.action.triggered && interactableTag != "") {
            StartInteraction(interactableTag);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Check if the layer of the object is Interactable
        if (other.gameObject.layer == LayerMask.NameToLayer(Interaction.INTERACTABLE_LAYER)) {  
            interactPanel.SetActive(true);
            interactableTag = other.gameObject.tag;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(Interaction.INTERACTABLE_LAYER)) {
            interactPanel.SetActive(true);
            interactableTag = other.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer(Interaction.INTERACTABLE_LAYER)) {
            interactPanel.SetActive(false);
            interactableTag = "";
        }
    }

    private void StartInteraction(string tag) {
        switch (tag) {
            case var value when value == Interaction.INTERACTABLE_TAG_CLASS:
                Debug.Log("Interacting with class");
                StartClass();
                break;
            default:
                Debug.Log("No interaction found");
                break;
        }
    }

    private void StartClass()
    {
        // Open the mini-game scene
        SceneManager.LoadScene(SceneName.SCENE_MINIGAME);
    }
}
