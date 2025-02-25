using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPC npc;

    public NPC Npc { get => npc; }
    
    [SerializeField] private SpriteRenderer npcSpriteRenderer;

    private GameDataManager _gameDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcSpriteRenderer.sprite = npc.npcSprite;
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    public void Interact()
    {

    }
}
