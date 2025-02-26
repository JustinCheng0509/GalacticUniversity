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
            default:
                CompleteQuest(quest);
                break;
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
