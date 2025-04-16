using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIQuestItemController : MonoBehaviour
{
    private Quest _quest;
    [SerializeField] private TMP_Text questTitleText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private TMP_Text questProgressText;

    private OverworldUIQuestColumnController _questColumnController;
    private OverworldUILayoutController _layoutController;

    private GameDataManager _gameDataManager;

    void Start()
    {
        _questColumnController = FindAnyObjectByType<OverworldUIQuestColumnController>();
        _questColumnController.OnQuestSelected += HandleQuestSelected;

        _layoutController = FindAnyObjectByType<OverworldUILayoutController>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnPotentialQuestProgressUpdated += UpdateQuestProgressText;

        ToggleQuestDescription(false);
    }

    void OnEnable()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected += HandleQuestSelected;
        }
        if (_gameDataManager != null)
        {
            _gameDataManager.OnPotentialQuestProgressUpdated += UpdateQuestProgressText;
        }
    }

    void OnDisable()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected -= HandleQuestSelected;
        }
        if (_gameDataManager != null)
        {
            _gameDataManager.OnPotentialQuestProgressUpdated -= UpdateQuestProgressText;
        }
    }

    void OnDestroy()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected -= HandleQuestSelected;
        }
        if (_gameDataManager != null)
        {
            _gameDataManager.OnPotentialQuestProgressUpdated -= UpdateQuestProgressText;
        }
    }

    public void SetQuest(Quest quest)
    {
        if (_gameDataManager == null) {
            _gameDataManager = FindAnyObjectByType<GameDataManager>();
        }
        
        _quest = quest;
        questTitleText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;
        UpdateQuestProgressText();
        
    }

    private void UpdateQuestProgressText()
    {
        if (_quest == null) return;
        string progressText = "";
        // Debug.Log("Quest Type: " + quest.questType);
        if (_quest.questType == QuestType.ScoreTotal) progressText = "Progress: " + _gameDataManager.ScoreDataManager.TotalScore + "/" + (int) _quest.targetValue;
        if (_quest.questType == QuestType.NumberOfItemsBought) progressText = "Progress: " + _gameDataManager.NumberOfItemsBought + "/" + (int) _quest.targetValue;
        if (_quest.questType == QuestType.Attendance) progressText = "Progress: " + _gameDataManager.NumberOfAttendances + "/" + (int) _quest.targetValue;
        // if (quest.questType == QuestType.ItemDelivery) return "Progress: " + _gameDataManager.GetItemCount(quest.itemID) + "/" + (int) quest.targetValue;
        questProgressText.text = progressText;
    }

    public void OnClick()
    {
        if (_questColumnController == null) return;

        if (_questColumnController.SelectedQuest == null || _questColumnController.SelectedQuest.questID != _quest.questID)
        {
            _questColumnController.SelectedQuest = _quest;
        }
        else
        {
            _questColumnController.SelectedQuest = null;
        }
    }

    public void HandleQuestSelected(Quest quest)
    {
        if (_questColumnController == null) return;
        if (quest == null) {
            ToggleQuestDescription(false);
            return;
        }
        UpdateQuestProgressText();
        ToggleQuestDescription(quest.questID == _quest.questID);
    }

    private void ToggleQuestDescription(bool isShown)
    {
        questDescriptionText.gameObject.SetActive(isShown);
        questProgressText.gameObject.SetActive(isShown);

        _layoutController.ForceLayoutRebuild();
    }
}
