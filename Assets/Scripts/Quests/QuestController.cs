using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private DialogController _dialogController;

    public event System.Action<Quest> OnQuestCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _dialogController = FindAnyObjectByType<DialogController>();
    }

    public void AddQuest(Quest quest)
    {
        _gameDataManager.AddQuest(quest);
    }

    public void AddQuest(string questID)
    {
        Quest quest = _gameDataManager.QuestList.Find(q => q.questID == questID);
        if (quest != null)
        {
            AddQuest(quest);
        }
    }   

    public void AddQuests(List<Quest> quests)
    {
        foreach (Quest quest in quests)
        {
            AddQuest(quest);
        }
    }

    public void TryReturnQuest(string questID)
    {
        if (_gameDataManager == null) {
            Debug.LogWarning("GameDataManager is not initialized.");
            return;
        }
        
        Quest quest = _gameDataManager.GetActiveQuests().Find(q => q.questID == questID);
        if (quest == null)
        {
            Debug.LogWarning("Quest " + questID + " is not in the active quests list.");
            return;
        }
        
        TryReturnQuest(quest);
    }

    public void TryReturnQuest(Quest quest)
    {
        // Check if quest is in the active quests list
        if (!_gameDataManager.IsQuestActive(quest.questID)) {
            Debug.LogWarning("Quest " + quest.questID + " is not in the active quests list.");
            return;
        }

        if (CheckQuestCompletion(quest))
        {
            HandleQuestCompletion(quest);
        } else {
            _dialogController.SetDialog(quest.incompleteDialog);
        }
    }

    private void HandleQuestCompletion(Quest quest)
    {
        switch (quest.questType)
        {
            case QuestType.ItemDelivery:
                _gameDataManager.InventoryManager.RemoveItem(quest.itemID, (int) quest.targetValue);
                _dialogController.SetDialog(quest.completeDialog);
                CompleteQuest(quest);
                break;
            case QuestType.ScoreTotal:
            case QuestType.NumberOfItemsBought:
            case QuestType.Attendance:
            default:
                _dialogController.SetDialog(quest.completeDialog);
                CompleteQuest(quest);
                break;
        }
    }

    public bool CheckQuestCompletion(Quest quest)
    {
        // Check if quest is in the active quests list
        if (!_gameDataManager.IsQuestActive(quest.questID)) {
            Debug.LogWarning("Quest " + quest.questID + " is not in the active quests list.");
            return false;
        }
        // Switch statement to check the quest type
        switch (quest.questType)
        {
            case QuestType.ScoreTotal:
                return CheckScoreTotalQuest(quest);
            case QuestType.NumberOfItemsBought:
                return CheckNumberOfItemsBoughtQuest(quest);
            case QuestType.Attendance:
                return CheckAttendanceQuest(quest);
            case QuestType.ItemDelivery:
                return CheckItemDeliveryQuest(quest);
            case QuestType.TotalWorkHours:
                return CheckTotalWorkHoursQuest(quest);
            case QuestType.TotalChestsOpened:
                return CheckTotalChestsOpenedQuest(quest);
            default:
                return true; // Default case if no specific type is matched
        }
    }

    private bool CheckTotalChestsOpenedQuest(Quest quest)
    {
        return _gameDataManager.OpenedChests.Count >= quest.targetValue;
    }

    private bool CheckTotalWorkHoursQuest(Quest quest)
    {
        return _gameDataManager.TotalWorkshopMinutes/60 >= quest.targetValue;
    }

    private bool CheckItemDeliveryQuest(Quest quest)
    {
        return _gameDataManager.InventoryManager.GetItemCount(quest.itemID) >= quest.targetValue;
    }

    private bool CheckNumberOfItemsBoughtQuest(Quest quest)
    {
        return _gameDataManager.NumberOfItemsBought >= quest.targetValue;
    }

    private bool CheckAttendanceQuest(Quest quest)
    {
        return _gameDataManager.NumberOfAttendances >= (int) quest.targetValue;
    }

    private bool CheckScoreTotalQuest(Quest quest)
    {
        return _gameDataManager.ScoreDataManager.TotalScore >= quest.targetValue;
    }

    private void CompleteQuest(Quest quest)
    {
        _gameDataManager.CompleteQuest(quest);
        // Reward the player
        _gameDataManager.Money += quest.rewardMoney;
        if (quest.rewardItem != null)
        {
            _gameDataManager.InventoryManager.AddItem(quest.rewardItem);
        }
        OnQuestCompleted?.Invoke(quest);
    }
}
