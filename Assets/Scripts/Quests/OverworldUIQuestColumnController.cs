using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIQuestColumnController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private OverworldUILayoutController _layoutController;

    [SerializeField] private GameObject _questItemPrefab;

    private Quest _selectedQuest;

    public Quest SelectedQuest 
    {
        get => _selectedQuest;
        set
        {
            _selectedQuest = value;
            OnQuestSelected?.Invoke(_selectedQuest);
        }
    }

    public event Action<Quest> OnQuestSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnActiveQuestsUpdated += HandleQuestUpdate;
        _layoutController = FindAnyObjectByType<OverworldUILayoutController>();
    }

    private void HandleQuestUpdate(List<Quest> quests)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        SelectedQuest = null;

        foreach (Quest quest in quests)
        {
            GameObject questItem = Instantiate(_questItemPrefab, transform);
            questItem.GetComponent<OverworldUIQuestItemController>().SetQuest(quest);
            // Set the quest item's parent to this column
            questItem.transform.SetParent(transform);
        }
        
        _layoutController.ForceLayoutRebuild();
    }
}
