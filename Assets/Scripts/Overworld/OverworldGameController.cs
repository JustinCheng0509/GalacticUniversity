using UnityEngine;

public class OverworldGameController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private SwitchScene _switchScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;

        _switchScene = FindAnyObjectByType<SwitchScene>();
    }

    private void OnGameDataLoaded()
    {
        _switchScene.FadeInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
