using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Dialogue[] introDialogues;

    public Dialogue[] notClassTimeDialogues;

    public Dialogue[] alreadyAttendedDialogues;

    [SerializeField]
    private GameObject dialoguePanel;

    [SerializeField]
    private TMP_Text dialogueText;

    private Dialogue[] currentDialogues;
    private int currentDialogueIndex = 0;

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
