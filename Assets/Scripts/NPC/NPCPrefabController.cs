using UnityEngine;

public class NPCPrefabController : MonoBehaviour
{
    [SerializeField] private NPC npc;

    public NPC Npc { get => npc; }
    
    [SerializeField] private SpriteRenderer npcSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcSpriteRenderer.sprite = npc.npcSprite;
    }
}
