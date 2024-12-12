using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OverworldGlobalLight : MonoBehaviour
{
    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private OverworldTimeController timeController;

    private Light2D overworldLight;



    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        overworldLight = GetComponent<Light2D>();
        float percentage = timeController.GetTimePercentage();
        overworldLight.color = gradient.Evaluate(percentage);
    }

    // Update is called once per frame
    void Update()
    {
        float percentage = timeController.GetTimePercentage();
        overworldLight.color = gradient.Evaluate(percentage);
    }
}
