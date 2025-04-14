using UnityEngine;

public class NPCPrefabController : MonoBehaviour
{
    [SerializeField] private NPC _npc;
    public NPC Npc { get => _npc; }
    
    [SerializeField] private SpriteRenderer _npcSpriteRenderer;
    [SerializeField] private SpriteRenderer _npcMinimapSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _npcSpriteRenderer.sprite = _npc.npcSprite;
        _npcMinimapSpriteRenderer.sprite = _npc.npcSprite;
    }
}
