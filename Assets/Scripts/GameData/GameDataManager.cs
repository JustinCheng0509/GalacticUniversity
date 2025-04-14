using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameDataManager : MonoBehaviour
{
    private List<Dialog> _dialogList = new List<Dialog>();
    private List<Tutorial> _tutorialList = new List<Tutorial>();
    private List<Quest> _questList = new List<Quest>();
    private List<NPC> _npcList = new List<NPC>();
    private List<Chest> _chestList = new List<Chest>();

    private GameData _gameData;

    public event Action OnGameDataLoaded;

    // Getters and Setters for GameData properties
    public GameData GameData => _gameData;

    public event Action<AttendanceStatus> OnAttendanceUpdated;
    public event Action<float> OnHomeworkProgressUpdated;
    public event Action<float> OnEnergyUpdated;
    public event Action<float> OnHungerUpdated;
    public event Action<float> OnMoodUpdated;
    public event Action<float> OnManeuverabilityUpdated;
    public event Action<float> OnDestructionUpdated;
    public event Action<float> OnMechanicsUpdated;
    public event Action<float> OnMoneyUpdated;
    public event Action<string> OnTimeUpdated;
    public event Action<int> OnDayUpdated;
    public event Action<List<Quest>> OnActiveQuestsUpdated;
    public event Action<Quest> OnQuestCompleted;
    public event Action<NPC> OnNPCRelationshipUpdated;
    public event Action<List<Item>> OnInventoryUpdated;
    public event Action<string> OnChestOpened;

    public List<Dialog> DialogList => _dialogList;
    public List<Tutorial> TutorialList => _tutorialList;
    public List<Quest> QuestList => _questList;
    public List<NPC> NPCList => _npcList;

    public float TotalWorkshopMinutes 
    {
        get => _gameData.totalWorkshopMinutes;
        set => _gameData.totalWorkshopMinutes = value;
    }

    public float workshopMoneyBonus
    {
        get => _gameData.workshopMoneyBonus;
        set => _gameData.workshopMoneyBonus = value;
    }

    public float MoveSpeedBonus
    {
        get => _gameData.moveSpeedBonus;
        set => _gameData.moveSpeedBonus = value;
    }

    public float MinigameMoveSpeedBonus
    {
        get => _gameData.minigameMoveSpeedBonus;
        set => _gameData.minigameMoveSpeedBonus = value;
    }

    public int NumberOfItemsBought
    {
        get => _gameData.numberOfItemsBought;
        set => _gameData.numberOfItemsBought = value;
    }
    
    public string PlayerName
    {
        get => _gameData.playerName;
        set => _gameData.playerName = value;
    }

    public float Energy
    {
        get => _gameData.energy;
        set {
            _gameData.energy = value;
            _gameData.energy = Mathf.Clamp(_gameData.energy, 0, 100);
            OnEnergyUpdated?.Invoke(_gameData.energy);
        }
        
    }

    public float Hunger
    {
        get => _gameData.hunger;
        set {
            _gameData.hunger = value;
            _gameData.hunger = Mathf.Clamp(_gameData.hunger, 0, 100);
            OnHungerUpdated?.Invoke(_gameData.hunger);
        }
    }

    public float Mood
    {
        get => _gameData.mood;
        set {
            _gameData.mood = value;
            _gameData.mood = Mathf.Clamp(_gameData.mood, 0, 100);
            OnMoodUpdated?.Invoke(_gameData.mood);
        }
    }

    public float Money
    {
        get => _gameData.money;
        set {
            _gameData.money = value;
            OnMoneyUpdated?.Invoke(_gameData.money);
        }
    }

    public float Maneuverability
    {
        get => _gameData.maneuverability;
        set {
            _gameData.maneuverability = value;
            _gameData.maneuverability = Mathf.Clamp(_gameData.maneuverability, 0, 100);
            OnManeuverabilityUpdated?.Invoke(_gameData.maneuverability);
        }
    }

    public float Destruction
    {
        get => _gameData.destruction;
        set {
            _gameData.destruction = value;
            _gameData.destruction = Mathf.Clamp(_gameData.destruction, 0, 100);
            OnDestructionUpdated?.Invoke(_gameData.destruction);
        }
    }

    public float Mechanics
    {
        get => _gameData.mechanics;
        set {
            _gameData.mechanics = value;
            _gameData.mechanics = Mathf.Clamp(_gameData.mechanics, 0, 100);
            OnMechanicsUpdated?.Invoke(_gameData.mechanics);
        }
    }

    public string CurrentTime
    {
        get => _gameData.currentTime;
        set {
            _gameData.currentTime = value;
            OnTimeUpdated?.Invoke(_gameData.currentTime);
        }
    }

    public int CurrentDay
    {
        get => _gameData.currentDay;
        set {
            _gameData.currentDay = value;
            OnDayUpdated?.Invoke(_gameData.currentDay);
            OnAttendanceUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].attendance);
            OnHomeworkProgressUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress);
        }
    }

    public int TotalNumberOfDays
    {
        get => _gameData.totalNumberOfDays;
        set => _gameData.totalNumberOfDays = value;
    }

    public bool IsTutorialEnabled
    {
        get => _gameData.isTutorialEnabled;
        set => _gameData.isTutorialEnabled = value;
    }

    public bool IntroDialogPlayed
    {
        get => _gameData.introDialogPlayed;
        set => _gameData.introDialogPlayed = value;
    }

    public int TotalScore
    {
        get => _gameData.totalScore;
        set => _gameData.totalScore = value;
    }

    public int TotalDestructionScore
    {
        get => _gameData.totalDestructionScore;
        set => _gameData.totalDestructionScore = value;
    }

    public int TotalSafetyScore
    {
        get => _gameData.totalSafetyScore;
        set => _gameData.totalSafetyScore = value;
    }

    public float LeaningSpeedBonus {
        get => _gameData.learningSpeedBonus;
        set => _gameData.learningSpeedBonus = value;
    }

    public List<Quest> GetActiveQuests()
    {
        return _gameData.activeQuests;
    }

    public bool IsQuestActive(string id)
    {
        return _gameData.activeQuests.Exists(quest => quest.questID == id);
    }

    public List<string> GetCompletedQuestIDs()
    {
        return _gameData.completedQuestIDs;
    }

    public void AddQuest(Quest quest)
    {
        // if quest is already in the list, don't add it again
        if (_gameData.activeQuests.Contains(quest))
        {
            return;
        }
        _gameData.activeQuests.Add(quest);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }

    public void CompleteQuest(Quest quest)
    {
        if (!_gameData.activeQuests.Contains(quest))
        {
            return;
        }
        _gameData.activeQuests.Remove(quest);
        _gameData.completedQuestIDs.Add(quest.questID);
        OnQuestCompleted?.Invoke(quest);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }

    public void RemoveQuest(Quest quest)
    {
        _gameData.activeQuests.Remove(quest);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }

    public List<NPCRelationshipKeyValuePair> NPCRelationships => _gameData.npcRelationships;

    public float GetNPCRelationship(string npcId)
    {
        NPCRelationshipKeyValuePair n = _gameData.npcRelationships.Find(n => n.npcID == npcId);
        if (n != null)
        {
            return n.relationshipValue;
        }
        // Create a new relationship if it doesn't exist
        n = new NPCRelationshipKeyValuePair { npcID = npcId, relationshipValue = 0 };
        _gameData.npcRelationships.Add(n);
        return 0;
    }

    public bool IsNPCRelationshipExists(string npcId)
    {
        return _gameData.npcRelationships.Exists(n => n.npcID == npcId);
    }

    public float GetNPCRelationship(NPC npc)
    {
        return GetNPCRelationship(npc.npcID);
    }

    public string GetNPCName(string npcId)
    {
        NPC npc = _npcList.Find(n => n.npcID == npcId);
        return npc != null ? npc.npcName : "Unknown NPC";
    }

    public void UpdateNPCRelationship(string npcId, float relationshipChange)
    {
        NPCRelationshipKeyValuePair n = _gameData.npcRelationships.Find(n => n.npcID == npcId);
        if (n == null)
        {
            n = new NPCRelationshipKeyValuePair { npcID = npcId, relationshipValue = 0 };
            n.relationshipValue = Mathf.Clamp(n.relationshipValue, 0, 100);
            _gameData.npcRelationships.Add(n);
        } else {
            n.relationshipValue += relationshipChange;  
            n.relationshipValue = Mathf.Clamp(n.relationshipValue, 0, 100);
            // Update the relationship value in the list
            _gameData.npcRelationships[_gameData.npcRelationships.IndexOf(n)] = n;
        }
        OnNPCRelationshipUpdated?.Invoke(_npcList.Find(n => n.npcID == npcId));
    }

    public List<DailyGameData> DailyGameDataList
    {
        get => _gameData.dailyGameDataList;
        set => _gameData.dailyGameDataList = value;
    }

    public List<LeaderboardEntry> Leaderboard
    {
        get => _gameData.leaderboard;
        set => _gameData.leaderboard = value;
    }

    public AttendanceStatus Attendance
    {
        get => _gameData.dailyGameDataList[_gameData.currentDay - 1].attendance;
        set
        {
            _gameData.dailyGameDataList[_gameData.currentDay - 1].attendance = value;
            OnAttendanceUpdated?.Invoke(value);
        }
    }

    public float HomeworkProgress
    {
        get => _gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress;
        set
        {
            _gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress = value;
            _gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress = Mathf.Clamp(_gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress, 0, 100);
            OnHomeworkProgressUpdated?.Invoke(value);
        }
    }

    public float ShopItemDiscount 
    {
        get => _gameData.shopItemDiscount;
        set => _gameData.shopItemDiscount = value;
    }

    public List<Item> Inventory => _gameData.inventory;

    public void AddItemToInventory(Item item)
    {
        _gameData.inventory.Add(item);
        if (item.hasPassiveEffect) {
            if (item.overworldMoveSpeedBonus > 0) {
                MoveSpeedBonus += item.overworldMoveSpeedBonus;
            }
            if (item.minigameMoveSpeedBonus > 0) {
                MinigameMoveSpeedBonus += item.minigameMoveSpeedBonus;
            }
            if (item.learningSpeedBonus > 0) {
                LeaningSpeedBonus += item.learningSpeedBonus;
            }
            if (item.shopItemDiscount > 0) {
                ShopItemDiscount += item.shopItemDiscount;
            }
        }
        OnInventoryUpdated?.Invoke(_gameData.inventory);
    }

    public void RemoveItemFromInventory(string itemId)
    {
        Item item = _gameData.inventory.Find(i => i.itemID == itemId);
        if (item != null)
        {
            RemoveItemFromInventory(item);
        }
    }

    public void RemoveItemFromInventory(Item item)
    {
        _gameData.inventory.Remove(item);
        OnInventoryUpdated?.Invoke(_gameData.inventory);
    }
    
    public bool IsTutorialCompleted(string tutorialId)
    {
        return _gameData.tutorialsCompleted.Contains(tutorialId);
    }

    public void CompleteTutorial(string tutorialId)
    {
        if (!_gameData.tutorialsCompleted.Contains(tutorialId))
        {
            _gameData.tutorialsCompleted.Add(tutorialId);
        }
    }

    public bool IsChestOpened(string chestId)
    {
        return _gameData.openedChests.Contains(chestId);
    }

    public void OpenChest(Chest chest)
    {
        if (!_gameData.openedChests.Contains(chest.chestID))
        {
            // Add the chest to the list of opened chests
            _gameData.openedChests.Add(chest.chestID);
            // Add the items to the inventory
            foreach (Item item in chest.items)
            {
                AddItemToInventory(item);
            }
            OnChestOpened?.Invoke(chest.chestID);
        }
    }

    async void Start()
    {
        // Load game data first
        _gameData = SavedDataManager.LoadGameData();

        // Start loading all assets concurrently
        Task<List<Dialog>> dialogTask = AddressableLoader.LoadAllAssets<Dialog>(AddressableLabels.ADDRESSABLE_LABEL_DIALOGS);
        Task<List<Tutorial>> tutorialTask = AddressableLoader.LoadAllAssets<Tutorial>(AddressableLabels.ADDRESSABLE_LABEL_TUTORIALS);
        Task<List<Quest>> questTask = AddressableLoader.LoadAllAssets<Quest>(AddressableLabels.ADDRESSABLE_LABEL_QUESTS);
        Task<List<NPC>> npcTask = AddressableLoader.LoadAllAssets<NPC>(AddressableLabels.ADDRESSABLE_LABEL_NPCS);
        // Wait until all tasks are completed
        await Task.WhenAll(dialogTask, tutorialTask, questTask, npcTask);

        // Assign the results
        _dialogList = dialogTask.Result;
        _tutorialList = tutorialTask.Result;
        _questList = questTask.Result;
        _npcList = npcTask.Result;

        foreach (Dialog dialog in _dialogList) {
            Debug.Log(dialog.text);
            Debug.Log(dialog.associatedTutorials.Count);
        }

        // Now all assets are loaded, fire game data event
        OnGameDataLoaded?.Invoke();

        // Update game state
        OnAttendanceUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].attendance);
        OnHomeworkProgressUpdated?.Invoke(_gameData.dailyGameDataList[_gameData.currentDay - 1].homeworkProgress);
        OnEnergyUpdated?.Invoke(_gameData.energy);
        OnHungerUpdated?.Invoke(_gameData.hunger);
        OnMoodUpdated?.Invoke(_gameData.mood);
        OnManeuverabilityUpdated?.Invoke(_gameData.maneuverability);
        OnDestructionUpdated?.Invoke(_gameData.destruction);
        OnMechanicsUpdated?.Invoke(_gameData.mechanics);
        OnMoneyUpdated?.Invoke(_gameData.money);
        OnTimeUpdated?.Invoke(_gameData.currentTime);
        OnDayUpdated?.Invoke(_gameData.currentDay);
        OnActiveQuestsUpdated?.Invoke(_gameData.activeQuests);
    }
}
