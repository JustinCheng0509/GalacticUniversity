using UnityEngine;

public enum QuestType
{
    VisitRoom,
    ItemDelivery,
    NumberOfItemsBought,
    Attendance,
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
    public QuestType questType;
    public float targetValue;
    public float currentValue;
    public string itemID; // For item delivery quests
    public string npcID; // For item delivery quests
    public Dialog startDialog;
    public Dialog incompleteDialog;
    public Dialog completeDialog;
    public float rewardMoney;
    public Item rewardItem;
}
