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
        if (!_gameDataManager.GetActiveQuests().Contains(quest)) {
            Debug.LogWarning("Quest " + quest.questID + " is not in the active quests list.");
            return;
        }
        // Switch statement to check the quest type
        switch (quest.questType)
        {
            case QuestType.ScoreTotal:
                CheckScoreTotalQuest(quest);
                break;
            case QuestType.NumberOfItemsBought:
                CheckNumberOfItemsBoughtQuest(quest);
                break;
            case QuestType.Attendance:
                CheckAttendanceQuest(quest);
                break;
            case QuestType.ItemDelivery:
                CheckItemDeliveryQuest(quest);
                break;
            default:
                CompleteQuest(quest);
                break;
        }
    }

    private void CheckItemDeliveryQuest(Quest quest)
    {
        int numberOfItems = 0;
        foreach (Item item in _gameDataManager.Inventory)
        {
            if (item.itemID == quest.itemID)
            {
                numberOfItems++;
            }
        }
        if (numberOfItems >= quest.targetValue)
        {
            // Remove number of items from inventory
            for (int i = 0; i < quest.targetValue; i++)
            {
                _gameDataManager.RemoveItemFromInventory(quest.itemID);
            }
            _dialogController.SetDialog(quest.completeDialog);
            CompleteQuest(quest);
        }
        else
        {
            _dialogController.SetDialog(quest.incompleteDialog);
        }
    }

    private void CheckNumberOfItemsBoughtQuest(Quest quest)
    {
        if (_gameDataManager.NumberOfItemsBought >= quest.targetValue)
        {
            _dialogController.SetDialog(quest.completeDialog);
            CompleteQuest(quest);
        }
        else
        {
            _dialogController.SetDialog(quest.incompleteDialog);
        }
    }

    private void CheckAttendanceQuest(Quest quest)
    {
        // Count the number of days the player has attended
        int attendance = 0;
        foreach (var day in _gameDataManager.DailyGameDataList)
        {
            if (day.attendance == AttendanceStatus.ATTENDED)
            {
                attendance++;
            }
        }
        if (attendance >= quest.targetValue)
        {
            _dialogController.SetDialog(quest.completeDialog);
            CompleteQuest(quest);
        }
        else
        {
            _dialogController.SetDialog(quest.incompleteDialog);
        }
    }

    private void CompleteQuest(Quest quest)
    {
        _gameDataManager.CompleteQuest(quest);
        // Reward the player
        _gameDataManager.Money += quest.rewardMoney;
        if (quest.rewardItem != null)
        {
            _gameDataManager.AddItemToInventory(quest.rewardItem);
        }
        OnQuestCompleted?.Invoke(quest);
    }

    private void CheckScoreTotalQuest(Quest quest)
    {
        if (_gameDataManager.TotalScore >= quest.targetValue)
        {
            _dialogController.SetDialog(quest.completeDialog);
            CompleteQuest(quest);
        }
        else
        {
            _dialogController.SetDialog(quest.incompleteDialog);
        }
    }
}
