using UnityEngine;

public class UILeaderboardController : MonoBehaviour
{
    private GameDataManager _gameDataManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();    
    }

    private void OpenLeaderboardUI()
    {
        
    }
}
