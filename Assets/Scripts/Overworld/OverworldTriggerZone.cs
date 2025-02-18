using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    private DialogController dialogController;

    [SerializeField]
    private OverworldUIController overworldUIController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_CLASS && !PlayerPrefs.HasKey("introClassPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.classroomTriggerDialogs);
            PlayerPrefs.SetInt("introClassPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_SHOP && !PlayerPrefs.HasKey("introShopPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.shopTriggerDialogs);
            PlayerPrefs.SetInt("introShopPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_DORM && !PlayerPrefs.HasKey("introDormPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.dormTriggerDialogs);
            PlayerPrefs.SetInt("introDormPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_PLAYROOM && !PlayerPrefs.HasKey("introPlayRoomPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.playRoomTriggerDialogs);
            PlayerPrefs.SetInt("introPlayRoomPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == GameConstants.TRIGGER_TAG_WORK && !PlayerPrefs.HasKey("introWorkPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.workTriggerDialogs);
            PlayerPrefs.SetInt("introWorkPlayed", 1);
        }
    }
}
