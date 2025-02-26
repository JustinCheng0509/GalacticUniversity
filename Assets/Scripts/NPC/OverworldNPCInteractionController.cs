using System;
using UnityEngine;

public class OverworldNPCInteractionController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private DialogController _dialogController;
    private QuestController _questController;

    private NPC _currentNPC;
    public NPC CurrentNPC => _currentNPC;

    public event Action<NPC> OnNPCInteractionStarted;
    public event Action OnNPCStartChat;
    public event Action OnNPCQuestAttempted;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _dialogController = FindAnyObjectByType<DialogController>();
        _questController = FindAnyObjectByType<QuestController>();
    }

    public void StartNPCInteraction(NPC npc)
    {
        Time.timeScale = 0;
        _currentNPC = npc;
        // If player has never interacted with this NPC before, show the dialog
        if (!_gameDataManager.NPCRelationships.ContainsKey(npc.npcID))
        {
            _gameDataManager.NPCRelationships[npc.npcID] = 0;
            _dialogController.SetDialog(npc.introDialog);
        }
        
        OnNPCInteractionStarted?.Invoke(npc);
    }

    public void EndNPCInteraction()
    {
        Time.timeScale = 1;
        _currentNPC = null;
    }

    public void StartNPCChat()
    {
        Time.timeScale = 1;
        OnNPCStartChat?.Invoke();
    }

    public void AttemptNPCQuest()
    {
        OnNPCQuestAttempted?.Invoke();
        if (_currentNPC == null)
        {
            Debug.LogError("No NPC is currently being interacted with.");
            return;
        }

        if (_currentNPC.quests.Count == 0)
        {
            _dialogController.SetDialog(DialogIDs.DIALOG_NPC_NO_QUEST_AVAILABLE);
            return;
        }

        for (int i = 0; i < _currentNPC.quests.Count; i++)
        {
            Quest quest = _currentNPC.quests[i];
            if (_gameDataManager.GetCompletedQuestIDs().Contains(quest.questID))
            {
                // Continue to the next quest if this one is already completed
                continue;
            }

            if(_gameDataManager.IsQuestActive(quest.questID))
            {
                _questController.TryReturnQuest(quest);
                return;
            }

            // If relationship threshold is not met, show dialog
            int thresholdIndex = Math.Min(QuestIDs.QUEST_RELATIONSHIP_THRESHOLDS.Length - 1, i);
            if (_gameDataManager.GetNPCRelationship(_currentNPC.npcID) < QuestIDs.QUEST_RELATIONSHIP_THRESHOLDS[thresholdIndex])
            {
                _dialogController.SetDialog(DialogIDs.DIALOG_NPC_NOT_CLOSE_ENOUGH);
                return;
            } else {
                // If relationship threshold is met, show quest dialog
                _questController.AddQuest(quest);
                _dialogController.SetDialog(quest.startDialog);
                return;
            }
        }

        // If no active quest is found, show dialog
        _dialogController.SetDialog(DialogIDs.DIALOG_NPC_NO_QUEST_AVAILABLE);
    }
}
