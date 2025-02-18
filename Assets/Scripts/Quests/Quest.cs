using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string questDescription;
    public QuestType questType;
    public float targetValue;
    public int itemID; // For item delivery quests
    public int npcID; // For item delivery quests
    public bool isCompleted = false; // Track if the quest is completed
}
