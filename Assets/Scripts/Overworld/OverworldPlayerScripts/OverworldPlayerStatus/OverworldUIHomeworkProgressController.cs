using TMPro;
using UnityEngine;

public class OverworldUIHomeworkProgressController : MonoBehaviour
{
    private OverworldPlayerStatusController _overworldPlayerStatusController;

    [SerializeField] private TMP_Text _homeworkProgressText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindFirstObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnHomeworkProgressUpdated += HandleHomeworkProgressUpdate;
    }

    private void HandleHomeworkProgressUpdate(float homeworkProgress)
    {
        _homeworkProgressText.text = $"Homework Progress: {Mathf.RoundToInt(homeworkProgress)}%";
    }
}
