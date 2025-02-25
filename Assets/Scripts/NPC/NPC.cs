using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Scriptable Objects/NPC")]
public class NPC : ScriptableObject
{
    public string npcID;
    public string npcName;
    public string npcRace;
    public string npcDescription;
    public Sprite npcSprite;

    public Dialog introDialog;
    public Quest firstThresholdQuest;
    public Quest secondThresholdQuest;
    public Quest thirdThresholdQuest;
}
