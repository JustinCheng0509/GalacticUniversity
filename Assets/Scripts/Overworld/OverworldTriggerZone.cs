using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    private DialogueController dialogueController;

    [SerializeField]
    private OverworldUIController overworldUIController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_CLASS && !PlayerPrefs.HasKey("introClassPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.classroomTriggerDialogues);
            PlayerPrefs.SetInt("introClassPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_SHOP && !PlayerPrefs.HasKey("introShopPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.shopTriggerDialogues);
            PlayerPrefs.SetInt("introShopPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_DORM && !PlayerPrefs.HasKey("introDormPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.dormTriggerDialogues);
            PlayerPrefs.SetInt("introDormPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_PLAYROOM && !PlayerPrefs.HasKey("introPlayRoomPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.playRoomTriggerDialogues);
            PlayerPrefs.SetInt("introPlayRoomPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_WORK && !PlayerPrefs.HasKey("introWorkPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.workTriggerDialogues);
            PlayerPrefs.SetInt("introWorkPlayed", 1);
        }
    }
}
