using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUIQuestItemController : MonoBehaviour
{
    private Quest _quest;
    [SerializeField] private TMP_Text questTitleText;
    [SerializeField] private TMP_Text questDescriptionText;

    private OverworldUIQuestColumnController _questColumnController;
    private OverworldUILayoutController _layoutController;

    void Start()
    {
        _questColumnController = FindAnyObjectByType<OverworldUIQuestColumnController>();
        _questColumnController.OnQuestSelected += HandleQuestSelected;

        _layoutController = FindAnyObjectByType<OverworldUILayoutController>();

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
        _layoutController.ForceLayoutRebuild();
    }
}
