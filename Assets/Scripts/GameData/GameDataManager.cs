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
    public event Action OnPotentialQuestProgressUpdated;
    public event Action<Quest> OnQuestCompleted;
    public event Action<NPC> OnNPCRelationshipUpdated;
    public event Action<string> OnChestOpened;

    public List<Dialog> DialogList => _dialogList;
    public List<Tutorial> TutorialList => _tutorialList;
    public List<Quest> QuestList => _questList;
    public List<NPC> NPCList => _npcList;
    
    // Separate Manager classes for different data types
    private InventoryDataManager _inventoryManager = new InventoryDataManager();
    public InventoryDataManager InventoryManager => _inventoryManager;

    private ScoreDataManager _scoreDataManager = new ScoreDataManager();
    public ScoreDataManager ScoreDataManager => _scoreDataManager;

    public float OverworldPosX
    {
        get => _gameData.overworldPosX;
        set => _gameData.overworldPosX = value;
    }

    public float OverworldPosY
    {
        get => _gameData.overworldPosY;
        set => _gameData.overworldPosY = value;
    }

    public string CurrentScene
    {
        get => _gameData.currentScene;
        set => _gameData.currentScene = value;
    }

    public float TotalWorkshopMinutes 
    {
        get => _gameData.totalWorkshopMinutes;
        set => _gameData.totalWorkshopMinutes = value;
    }

    public float WorkshopMoneyBonus
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
        set
        {
            _gameData.numberOfItemsBought = value;
            OnPotentialQuestProgressUpdated?.Invoke();
        } 
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

    public int NumberOfAttendances
    {
        get {
            // Count the number of days the player has attended
            int attendance = 0;
            foreach (var day in DailyGameDataList)
            {
                if (day.attendance == AttendanceStatus.ATTENDED)
                {
                    attendance++;
                }
            }
            return attendance;
        }
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
            _gameData.openedChests.Add(chest.chestID);

            foreach (ChestItemEntry chestItemEntry in chest.chestItems)
            {
                InventoryManager.AddItem(chestItemEntry.item, chestItemEntry.quantity);
            }

            OnChestOpened?.Invoke(chest.chestID);
        }
    }

    public void GenerateLeaderboardScore()
    {
        // Generate the leaderboard score based on the player's stats
        _scoreDataManager.GenerateLeaderboardScore(_gameData.playerName, _gameData.currentDay, _gameData.mechanics, _gameData.destruction, _gameData.maneuverability);
    }

    private void OnInventoryUpdated(List<InventoryItem> inventory)
    {
        _gameData.inventory = inventory;
        _gameData.moveSpeedBonus = _inventoryManager.MoveSpeedBonus;
        _gameData.minigameMoveSpeedBonus = _inventoryManager.MinigameMoveSpeedBonus;
        _gameData.learningSpeedBonus = _inventoryManager.LearningSpeedBonus;
        _gameData.workshopMoneyBonus = _inventoryManager.WorkshopMoneyBonus;
        _gameData.shopItemDiscount = _inventoryManager.ShopItemDiscount;
    }

    private void OnScoreUpdated()
    {
        _gameData.totalScore = _scoreDataManager.TotalScore;
        _gameData.totalDamageDealt = _scoreDataManager.TotalDamageDealt;
        _gameData.dangersDestroyedScore = _scoreDataManager.DangersDestroyedScore;
        _gameData.totalDamageTaken = _scoreDataManager.TotalDamageTaken;
        _gameData.timesDead = _scoreDataManager.TimesDead;
        _gameData.leaderboard = _scoreDataManager.Leaderboard;
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
        
        // Initialize managers
        _inventoryManager.Initialize(_gameData.inventory);
        _inventoryManager.OnInventoryUpdated += OnInventoryUpdated;

        _scoreDataManager.Initialize(_gameData.totalScore, _gameData.totalDamageDealt, _gameData.dangersDestroyedScore, _gameData.totalDamageTaken,  _gameData.timesDead, _gameData.leaderboard);
        _scoreDataManager.OnScoreUpdated += OnScoreUpdated;

        // Now all assets are loaded, fire game data event
        OnGameDataLoaded?.Invoke();

        // Invoke all the events to update the game state
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
