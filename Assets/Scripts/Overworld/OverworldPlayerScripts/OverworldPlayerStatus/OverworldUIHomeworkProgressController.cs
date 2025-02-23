using TMPro;
using UnityEngine;

public class OverworldUIHomeworkProgressController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    [SerializeField] private TMP_Text _homeworkProgressText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnHomeworkProgressUpdated += HandleHomeworkProgressUpdate;
    }

    private void HandleHomeworkProgressUpdate(float homeworkProgress)
    {
        _homeworkProgressText.text = $"Homework Progress: {Mathf.RoundToInt(homeworkProgress)}%";
    }
}
