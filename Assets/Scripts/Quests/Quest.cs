using UnityEngine;

public enum QuestType
{
    VisitRoom,
    ItemDelivery,
    ScoreInRound,
    ScoreTotal,
    EnemiesDestroyedInRound,
    EnemiesDestroyedTotal,
    DamageDealtInRound,
    DamageDealtTotal,
    DamageTakenInRound,
    TotalManeuverability,
    TotalDestruction,
    TotalMechanics,
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string questID;
    public string questName;
    public string questDescription;
    public Dialog startDialog;
    public QuestType questType;
    public float targetValue;
    public int itemID; // For item delivery quests
    public int npcID;
    public bool isCompleted = false; // Track if the quest is completed
    public Dialog completeDialog;
    public float rewardMoney;
    public Item rewardItem;
}
