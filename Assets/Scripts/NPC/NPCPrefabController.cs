using UnityEngine;

public class NPCPrefabController : MonoBehaviour
{
    [SerializeField] private NPC _npc;
    public NPC Npc { get => _npc; }
    
    [SerializeField] private SpriteRenderer _npcSpriteRenderer;
    [SerializeField] private SpriteRenderer _npcMinimapSpriteRenderer;
    [SerializeField] private GameObject _navigationTag;

    void Awake()
    {
        _npcSpriteRenderer.sprite = _npc.npcSprite;
        _npcMinimapSpriteRenderer.sprite = _npc.npcSprite;
        // Set tag of _navigationTag to the npc name
        _navigationTag.name = _npc.npcID;
    }
}
