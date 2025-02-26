using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class OverworldUINPCPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _npcPanel;
    [SerializeField] private Image _npcImage;
    [SerializeField] private TMP_Text _npcName;
    [SerializeField] private TMP_Text _npcRace;
    [SerializeField] private TMP_Text _npcRelationship;
    [SerializeField] private TMP_Text _npcDescription;

    private OverworldNPCInteractionController _overworldNPCInteractionController;
    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldNPCInteractionController = FindAnyObjectByType<OverworldNPCInteractionController>();
        _overworldNPCInteractionController.OnNPCInteractionStarted += OnNPCInteractionStarted;
        _overworldNPCInteractionController.OnNPCStartChat += () => _npcPanel.SetActive(false);
        _overworldNPCInteractionController.OnNPCQuestAttempted += () => _npcPanel.SetActive(false);

        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    private void OnNPCInteractionStarted(NPC npc)
    {
        _npcPanel.SetActive(true);
        _npcImage.sprite = npc.npcSprite;
        _npcName.text = "Name: " + npc.npcName;
        _npcRace.text = "Race: " + npc.npcRace;
        _npcRelationship.text = $"Relationship: {(int) _gameDataManager.GetNPCRelationship(npc.npcID)}/100";
        _npcDescription.text = npc.npcDescription;
    }
}
