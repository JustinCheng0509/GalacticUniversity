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

        ToggleQuestDescription(false);
    }

    void OnEnable()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected += HandleQuestSelected;
        }
    }

    void OnDisable()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected -= HandleQuestSelected;
        }
    }

    void OnDestroy()
    {
        if (_questColumnController != null)
        {
            _questColumnController.OnQuestSelected -= HandleQuestSelected;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetQuest(Quest quest)
    {
        _quest = quest;
        questTitleText.text = quest.questName;
        questDescriptionText.text = quest.questDescription;
        if (quest.questType == QuestType.ScoreTotal)
        {
            questProgressText.text = "Progress: " + _gameDataManager.TotalScore + "/" + (int) quest.targetValue;
        }
    }

    public void OnClick()
    {
        if (_questColumnController == null) return;

        if (_questColumnController.SelectedQuest == _quest)
        {
            _questColumnController.SelectedQuest = null;
        }
        else
        {
            _questColumnController.SelectedQuest = _quest;
        }
    }

    public void HandleQuestSelected(Quest quest)
    {
        ToggleQuestDescription(quest == _quest);
    }

    private void ToggleQuestDescription(bool isShown)
    {
        questDescriptionText.gameObject.SetActive(isShown);
        if (_quest.questType == QuestType.ScoreTotal)
        {
            questProgressText.gameObject.SetActive(isShown);
        }
        else
        {
            questProgressText.gameObject.SetActive(false);
        }
        
        _layoutController.ForceLayoutRebuild();
    }
}
