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
    public event Action<Quest> OnQuestCompleted;
    public event Action<NPC> OnNPCRelationshipUpdated;
    public event Action<List<Item>> OnInventoryUpdated;

    public List<Dialog> DialogList => _dialogList;
    public List<Tutorial> TutorialList => _tutorialList;
    public List<Quest> QuestList => _questList;
    public List<NPC> NPCList => _npcList;
    
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

    public Dictionary<string, float> NPCRelationships => _gameData.npcRelationships;

    public float GetNPCRelationship(string npcId)
    {
        if (_gameData.npcRelationships.ContainsKey(npcId))
        {
            return _gameData.npcRelationships[npcId];
        }
        _gameData.npcRelationships[npcId] = 0; // Default relationship if not found
        return 0;
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
        if (_gameData.npcRelationships.ContainsKey(npcId))
        {
            _gameData.npcRelationships[npcId] += relationshipChange;
        }
        else
        {
            _gameData.npcRelationships[npcId] = relationshipChange;
        }
        
        _gameData.npcRelationships[npcId] = Mathf.Clamp(_gameData.npcRelationships[npcId], 0, 100);
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

    public List<Item> Inventory => _gameData.inventory;

    public void AddItemToInventory(Item item)
    {
        _gameData.inventory.Add(item);
        OnInventoryUpdated?.Invoke(_gameData.inventory);
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
