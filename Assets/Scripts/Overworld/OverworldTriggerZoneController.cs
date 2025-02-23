using UnityEngine;

public class OverworldTriggerZoneController : MonoBehaviour
{
    private DialogController _dialogController;
    private GameDataManager _gameDataManager;

    private void Start()
    {
        _dialogController = FindAnyObjectByType<DialogController>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            if (gameObject.tag == GameConstants.TRIGGER_TAG_DORM && !_gameDataManager.IntroDormPlayed) {
                _dialogController.SetDialog(DialogIDs.DIALOG_DORM_TRIGGER);
                _gameDataManager.IntroDormPlayed = true;
            }
        }
    }
}
