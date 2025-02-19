using UnityEngine;

public class OverworldTriggerZoneController : MonoBehaviour
{
    private DialogController _dialogController;

    private void Start()
    {
        _dialogController = FindAnyObjectByType<DialogController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_CLASS && !PlayerPrefs.HasKey("introClassPlayed"))
        {
            _dialogController.SetDialog(DialogIDs.CLASSROOM_TRIGGER_DIALOGS);
            PlayerPrefs.SetInt("introClassPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_SHOP && !PlayerPrefs.HasKey("introShopPlayed"))
        {
            _dialogController.SetDialog(DialogIDs.SHOP_TRIGGER_DIALOGS);
            PlayerPrefs.SetInt("introShopPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_DORM && !PlayerPrefs.HasKey("introDormPlayed"))
        {
            _dialogController.SetDialog(DialogIDs.DORM_TRIGGER_DIALOGS);
            PlayerPrefs.SetInt("introDormPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_PLAYROOM && !PlayerPrefs.HasKey("introPlayRoomPlayed"))
        {
            _dialogController.SetDialog(DialogIDs.PLAY_ROOM_TRIGGER_DIALOGS);
            PlayerPrefs.SetInt("introPlayRoomPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_WORK && !PlayerPrefs.HasKey("introWorkPlayed"))
        {
            _dialogController.SetDialog(DialogIDs.WORK_TRIGGER_DIALOGS);
            PlayerPrefs.SetInt("introWorkPlayed", 1);
        }
    }
}
