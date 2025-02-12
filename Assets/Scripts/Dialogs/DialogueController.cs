using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue[] introDialogues;

    public Dialogue[] notClassTimeDialogues;

    public Dialogue[] alreadyAttendedDialogues;

    public Dialogue[] classroomTriggerDialogues;

    public Dialogue[] dormTriggerDialogues;

    public Dialogue[] playRoomTriggerDialogues;

    public Dialogue[] workTriggerDialogues;

    public Dialogue[] shopTriggerDialogues;

    public Dialogue[] npcDialogues;

    public Dialogue[] notEnoughMoneyDialogues;

    public Dialogue[] tooTiredDialogues;

    public Dialogue[] tooStressed;

    public Dialogue[] tooHungry;

    public Dialogue[] homeworkDone;

    public Dialogue[] alreadyFull;

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TMP_Text dialogueText;

    private Dialogue[] currentDialogues;
    private int currentDialogueIndex = 0;

    [SerializeField] private TutorialController tutorialController;

    public void AdvanceDialogue()
    {
        if (currentDialogueIndex == currentDialogues.Length - 1) {
            EndDialogue();
            return;
        }

        currentDialogueIndex++;
        SetDialogue(currentDialogues[currentDialogueIndex]);
    }

    private bool CompareDialogs(Dialogue[] dialogue1, Dialogue[] dialogue2)
    {
        if (dialogue1.Length != dialogue2.Length) {
            return false;
        }

        for (int i = 0; i < dialogue1.Length; i++) {
            if (dialogue1[i].text != dialogue2[i].text) {
                return false;
            }
        }

        return true;
    }

    public void SetCurrentDialogues(Dialogue[] dialogues)
    {
        Time.timeScale = 0;
        currentDialogues = dialogues;
        currentDialogueIndex = 0;
        SetDialogue(currentDialogues[0]);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
        if (CompareDialogs(currentDialogues, introDialogues)) {
            tutorialController.ShowTutorial(tutorialController.startTutorial);
        } else if (CompareDialogs(currentDialogues, classroomTriggerDialogues)) {
            tutorialController.ShowTutorial(tutorialController.classTutorial);
        } else if (CompareDialogs(currentDialogues, shopTriggerDialogues)) {
            tutorialController.ShowTutorial(tutorialController.shopTutorial);
        } else if (CompareDialogs(currentDialogues, dormTriggerDialogues)) {
            tutorialController.ShowTutorial(tutorialController.homeworkTutorial);
        } else if (CompareDialogs(currentDialogues, workTriggerDialogues)) {
            tutorialController.ShowTutorial(tutorialController.workTutorial);
        } else if (CompareDialogs(currentDialogues, playRoomTriggerDialogues) && PlayerPrefs.GetInt("NeedTutorial", 0) == 0) {
            tutorialController.ShowTutorial(tutorialController.needTutorial);
        }
    }
    

    public void SetDialogue(Dialogue dialogue)
    {
        dialogueText.text = dialogue.text;

        if (dialogue.isSelfDialogue) {
            // italicize the text
            dialogueText.fontStyle = FontStyles.Italic;
        } else {
            // remove italicize
            dialogueText.fontStyle = FontStyles.Normal;
        }

        dialogueText.alignment = dialogue.isLeft ? TextAlignmentOptions.Left : TextAlignmentOptions.Right;
        
        dialoguePanel.SetActive(true);
    }
}
