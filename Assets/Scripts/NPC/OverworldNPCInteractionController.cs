using System;
using UnityEngine;

public class OverworldNPCInteractionController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private DialogController _dialogController;
    private NPC _currentNPC;
    public NPC CurrentNPC => _currentNPC;
    public event Action<NPC> OnNPCInteractionStarted;
    public event Action OnNPCStartChat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _dialogController = FindAnyObjectByType<DialogController>();
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
}
