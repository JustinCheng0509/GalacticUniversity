using UnityEngine;

public class OverworldOutdoorLight : MonoBehaviour
{
    private OverworldTimeController timeController;

    [SerializeField]
    private string turnOnTime = "19:00";

    [SerializeField]
    private string turnOffTime = "05:30";

    [SerializeField]
    private GameObject[] lights;

    private void Start() {
        timeController = FindAnyObjectByType<OverworldTimeController>();
    }

    private void Update() {
        if (timeController.CurrentTime == turnOnTime) {
            // Turn on the lights
            foreach (GameObject light in lights) {
                light.SetActive(true);
            }
        } else if (timeController.CurrentTime == turnOffTime) {
            // Turn off the lights
            foreach (GameObject light in lights) {
                light.SetActive(false);
            }
        }
    }
}
