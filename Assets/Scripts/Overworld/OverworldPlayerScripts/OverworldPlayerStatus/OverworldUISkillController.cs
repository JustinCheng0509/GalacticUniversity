using TMPro;
using UnityEngine;

public class OverworldUISkillController : MonoBehaviour
{
    [SerializeField] private TMP_Text maneuverabilityText;
    [SerializeField] private TMP_Text destructionText;
    [SerializeField] private TMP_Text mechanicsText;

    private OverworldPlayerStatusController _overworldPlayerStatusController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnSkillUpdated += HandleSkillUpdate;
    }

    void HandleSkillUpdate(float maneuverability, float destruction, float mechanics)
    {
        maneuverabilityText.text = Mathf.RoundToInt(maneuverability).ToString();
        destructionText.text = Mathf.RoundToInt(destruction).ToString();
        mechanicsText.text = Mathf.RoundToInt(mechanics).ToString();
    }
}
