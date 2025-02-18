using UnityEngine;

public enum QuestType
{
    ScoreInRound,
    ScoreTotal,
    EnemiesDestroyedInRound,
    EnemiesDestroyedTotal,
    DamageDealtInRound,
    DamageDealtTotal,
    DamageTakenInRound,
    TotalManeuverability,
    TotalDestruction,
    TotalMechanics,
}

public class QuestController : MonoBehaviour
{
    private Quest[] quests; // Array of all quests
    public Quest[] activeQuests; // Array of active quests

    private MinigameScoreController _minigameScoreController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        quests = Resources.LoadAll<Quest>("Quests"); // Load all quests from the Resources folder
        // Check if the current scene is the mini-game scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == GameConstants.SCENE_MINIGAME)
        {
            _minigameScoreController = FindFirstObjectByType<MinigameScoreController>();
            MinigameScoreController.OnScoreUpdated += CheckQuests;
        }
        
    }

    private void CheckQuests()
    {
        Debug.Log("Checking quests...");
    }
}
