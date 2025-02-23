using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private List<Quest> _quests = new List<Quest>(); // Array of all quests

    private GameDataManager _gameDataManager;

    public event System.Action<List<Quest>> OnQuestsUpdated;

    public event System.Action<Quest> OnQuestsCompleted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _quests.AddRange(Resources.LoadAll<Quest>("Quests"));
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;
        // Check if the current scene is the mini-game scene
        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GameConstants.SCENE_MINIGAME)
        // {
        //     _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        //     // MinigameScoreController.OnScoreUpdated += CheckQuests;
        // }
        
    }

    private void OnGameDataLoaded()
    {
        UpdateQuests();
    }

    public void AddQuest(Quest quest)
    {
        _gameDataManager.ActiveQuests.Add(quest);
        UpdateQuests();
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

    public void UpdateQuests()
    {
        OnQuestsUpdated?.Invoke(_gameDataManager.ActiveQuests);
    }

    public void TryReturnQuest(Quest quest)
    {
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
        _gameDataManager.ActiveQuests.Remove(quest);
        OnQuestsCompleted?.Invoke(quest);
        // Handle quest rewards
        // _gameDataManager.Money += quest.rewardMoney;
        // if (quest.rewardItem != null)
        // {
        //     _gameDataManager.AddItem(quest.rewardItem);
        // }

        UpdateQuests();
    }

    // private void CheckQuests()
    // {
    //     Debug.Log("Checking quests...");
    // }
}
