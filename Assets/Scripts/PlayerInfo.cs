using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInfo: MonoBehaviour
{
    public float energy;
    public float hunger;
    public float stress;
    public float maneuverability;
    public float destruction;
    public float mechanics;

    public bool isSleeping;
    public bool isDoingHomework;
    public bool isWorking;
    public bool isPlaying;

    [SerializeField]
    private TMP_Text energyText;
    [SerializeField]
    private TMP_Text hungerText;
    [SerializeField]
    private TMP_Text stressText;

    [SerializeField]
    private OverworldTimeController overworldTimeController;

    IEnumerator updateStatsCoroutine;

    public void Start()
    {
        // Check PlayerPrefs for saved data
        if (PlayerPrefs.HasKey("energy"))
        {
            energy = PlayerPrefs.GetFloat("energy");
            hunger = PlayerPrefs.GetFloat("hunger");
            stress = PlayerPrefs.GetFloat("stress");
            maneuverability = PlayerPrefs.GetFloat("maneuverability");
            destruction = PlayerPrefs.GetFloat("destruction");
            mechanics = PlayerPrefs.GetFloat("mechanics");
        }
        else
        {
            energy = 100;
            hunger = 100;
            stress = 0;
            maneuverability = 0;
            destruction = 0;
            mechanics = 0;
        }
        updateStatsCoroutine = UpdateStats();
        StartCoroutine(updateStatsCoroutine);
    }

    private void Update() {
        // Update the UI, Format to int
        energyText.text = "Energy: " + Mathf.RoundToInt(energy).ToString();
        hungerText.text = "Hunger: " + Mathf.RoundToInt(hunger).ToString();
        stressText.text = "Stress: " + Mathf.RoundToInt(stress).ToString();

        // Save the data
        PlayerPrefs.SetFloat("energy", energy);
        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetFloat("stress", stress);
        PlayerPrefs.SetFloat("maneuverability", maneuverability);
        PlayerPrefs.SetFloat("destruction", destruction);
        PlayerPrefs.SetFloat("mechanics", mechanics);
    }

    private IEnumerator UpdateStats()
    {
        while (true)
        {
            float timeInterval = overworldTimeController.intervalBetweenMinute;
            yield return new WaitForSeconds(timeInterval);
            if (!isSleeping){
                energy -= 0.05f;
            }
            hunger -= 0.05f;
            if (isWorking || isDoingHomework){
                stress += 0.2f;
            }
            if (isPlaying){
                stress -= 0.2f;
            }
        }
    }
}
