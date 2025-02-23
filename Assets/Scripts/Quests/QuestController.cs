using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> _quests = new List<Quest>(); // Array of all quests

    private GameDataManager _gameDataManager;

    public event System.Action<Quest> OnQuestCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _quests.AddRange(Resources.LoadAll<Quest>("Quests"));
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        // Check if the current scene is the mini-game scene
        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GameConstants.SCENE_MINIGAME)
        // {
        //     _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        //     // MinigameScoreController.OnScoreUpdated += CheckQuests;
        // }
        
    }

    public void AddQuest(Quest quest)
    {
        _gameDataManager.AddQuest(quest);
    }

    public void AddQuest(string questID)
    {
        Quest quest = _quests.Find(q => q.questID == questID);
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
            default:
                CompleteQuest(quest);
                break;
        }
    }

    private void CompleteQuest(Quest quest)
    {
        _gameDataManager.RemoveQuest(quest);
        OnQuestCompleted?.Invoke(quest);
        // Handle quest rewards
    }

    // private void CheckQuests()
    // {
    //     Debug.Log("Checking quests...");
    // }
}
