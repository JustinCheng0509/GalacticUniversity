using UnityEngine;

public class OverworldInteractionTrigger : MonoBehaviour
{
    private OverworldInteractionController _interactionController;

    private void Start() {
        _interactionController = FindAnyObjectByType<OverworldInteractionController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (IsInteractable(other)) {
            _interactionController.InteractableGameObject = other.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (IsInteractable(other)) {
            _interactionController.InteractableGameObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (IsInteractable(other)) {
            _interactionController.InteractableGameObject = null;
        }
    }

    private bool IsInteractable(Collider2D other) {
        return other.gameObject.layer == LayerMask.NameToLayer(GameConstants.INTERACTABLE_LAYER);
    }
}
