using UnityEngine;

public class OverworldOutdoorLight : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    private string _turnOnTime = "19:00";

    private string _turnOffTime = "05:30";

    private bool _isLightOn = false;

    [SerializeField] private GameObject[] lights;

    private void Start() {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnTimeUpdated += CheckOutdoorLightTime;
    }

    private void CheckOutdoorLightTime(string time) {
        if (OverworldTimeController.IsWithinTimeRange(_gameDataManager.CurrentTime, _turnOnTime, _turnOffTime)) {
            if (!_isLightOn) {
                _isLightOn = true;
                ToggleLights(true);
            }
        } else {
            if (_isLightOn) {
                _isLightOn = false;
                ToggleLights(false);
            }
        }
    }

    private void ToggleLights(bool isOn) {
        foreach (GameObject light in lights) {
            light.SetActive(isOn);
        }
    }
}
