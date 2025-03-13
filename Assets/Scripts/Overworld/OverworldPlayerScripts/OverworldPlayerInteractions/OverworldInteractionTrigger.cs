using UnityEngine;

public class OverworldInteractionTrigger : MonoBehaviour
{
    private OverworldInteractionController _interactionController;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip interactionSFX; // Assign in Inspector

    private void Start() {
        _interactionController = FindAnyObjectByType<OverworldInteractionController>();
        _audioSource = GetComponent<AudioSource>(); // Get AudioSource component
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

    // Plays SFX when the interaction button is pressed
    public void PlayInteractionSFX() {
        if (_audioSource != null && interactionSFX != null) {
            _audioSource.PlayOneShot(interactionSFX);
        }
    }
}
