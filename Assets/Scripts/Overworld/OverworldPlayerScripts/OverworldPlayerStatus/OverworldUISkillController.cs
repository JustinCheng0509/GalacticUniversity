using TMPro;
using UnityEngine;

public class OverworldUISkillController : MonoBehaviour
{
    [SerializeField] private TMP_Text maneuverabilityText;
    [SerializeField] private TMP_Text destructionText;
    [SerializeField] private TMP_Text mechanicsText;

    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnManeuverabilityUpdated += HandleManeuverabilityUpdate;
        _gameDataManager.OnDestructionUpdated += HandleDestructionUpdate;
        _gameDataManager.OnMechanicsUpdated += HandleMechanicsUpdate;
    }

    private void HandleManeuverabilityUpdate(float maneuverability)
    {
        maneuverabilityText.text = Mathf.RoundToInt(maneuverability).ToString();
    }

    private void HandleDestructionUpdate(float destruction)
    {
        destructionText.text = Mathf.RoundToInt(destruction).ToString();
    }

    private void HandleMechanicsUpdate(float mechanics)
    {
        mechanicsText.text = Mathf.RoundToInt(mechanics).ToString();
    }
}
