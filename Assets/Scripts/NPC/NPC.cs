using System.Collections.Generic;
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
    public List<Quest> quests;
}
