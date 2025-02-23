using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OverworldGlobalLight : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    private GameDataManager _gameDataManager;

    private Light2D _overworldLight;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnTimeUpdated += UpdateGlobalLight;
        _overworldLight = GetComponent<Light2D>();
    }

    private void UpdateGlobalLight(string time)
    {
        float percentage = OverworldTimeController.GetTimePercentage(time);
        _overworldLight.color = _gradient.Evaluate(percentage);
    }
}
