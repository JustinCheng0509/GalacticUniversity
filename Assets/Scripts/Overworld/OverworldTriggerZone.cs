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
            // overworldUIController.checkoutClassRoomToggle.isOn = true;
            dialogueController.SetCurrentDialogues(dialogueController.classroomTriggerDialogues);
            PlayerPrefs.SetInt("introClassPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_SHOP && !PlayerPrefs.HasKey("introShopPlayed"))
        {
            // overworldUIController.checkoutShopToggle.isOn = true;
            dialogueController.SetCurrentDialogues(dialogueController.shopTriggerDialogues);
            PlayerPrefs.SetInt("introShopPlayed", 1);
        }

        if (collision.CompareTag("Player") && gameObject.tag == StaticValues.TRIGGER_TAG_DORM && !PlayerPrefs.HasKey("introDormPlayed"))
        {
            // overworldUIController.checkoutDormToggle.isOn = true;
            dialogueController.SetCurrentDialogues(dialogueController.dormTriggerDialogues);
            PlayerPrefs.SetInt("introDormPlayed", 1);
        }
    }
}
