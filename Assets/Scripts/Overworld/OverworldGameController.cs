using UnityEngine;

public class OverworldGameController : MonoBehaviour
{
    [SerializeField] private GameDataManager gameDataManager;
    [SerializeField] private OverworldSwitchScene overworldSwitchScene;
    [SerializeField] private PlayerInfo playerInfo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set time scale to 0
        Time.timeScale = 0;
        // Load game data
        playerInfo.gameData = gameDataManager.LoadGameData();
        // Set time scale to 1
        Time.timeScale = 1;

        // Fade in the game
        overworldSwitchScene.FadeInGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
