using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum AttendanceStatus
{
    NOT_STARTED,
    ATTENDED,
    ABSENT
}

[System.Serializable]
public class NPCRelationshipKeyValuePair
{
    public string npcID;
    public float relationshipValue;
}

[System.Serializable]
public class DailyGameData
{
    public float homeworkProgress = 0;
    public AttendanceStatus attendance = AttendanceStatus.NOT_STARTED;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string name = "name";
    public int totalScore = 0;
}

[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;

    public InventoryItem(Item item)
    {
        this.item = item;
        this.quantity = 1;
    }

    public InventoryItem(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}


[System.Serializable]
public class GameData
{
    public string playerName = "Player";
    public float overworldPosX = -8.4f;
    public float overworldPosY = -2.74f;
    public string currentScene = GameConstants.SCENE_OVERWORLD;

    public float energy = 100;
    public float hunger = 100;
    public float mood = 100;
    public float money = 200;

    public float maneuverability = 0;
    public float destruction = 0;
    public float mechanics = 0;

    public string currentTime = GameConstants.NEW_GAME_START_TIME;
    public int currentDay = 1;
    public int totalNumberOfDays = 10;

    public int totalScore = 0;
    public int totalDamageDealt = 0;
    public int dangersDestroyedScore;
    public int totalDamageTaken = 0;
    public int timesDead = 0;

    public List<Quest> activeQuests = new List<Quest>();
    public List<string> completedQuestIDs = new List<string>();

    public List<NPCRelationshipKeyValuePair> npcRelationships = new List<NPCRelationshipKeyValuePair>();

    public List<string> tutorialsCompleted = new List<string>();

    public List<string> openedChests = new List<string>();

    public List<InventoryItem> inventory = new List<InventoryItem>();

    public List<DailyGameData> dailyGameDataList = new List<DailyGameData>();

    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();

    // Quest trackers
    public int numberOfItemsBought;
    public float totalWorkshopMinutes;

    // Bonus from items
    public float moveSpeedBonus;
    public float minigameMoveSpeedBonus;
    public float learningSpeedBonus;
    public float shopItemDiscount;
    public float workshopMoneyBonus;
    public float shipFireRateIncrease;
    public float shipDamageIncrease;
    public float shipDamageTakenDecrease;

    // Intros and tutorials
    public bool isTutorialEnabled = true;
    public bool introDialogPlayed = false;
    
}
