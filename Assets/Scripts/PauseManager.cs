using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private List<GameObject> pauseUIs = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Register a UI element that can pause the game
    public void RegisterPauseUI(GameObject ui)
    {
        if (!pauseUIs.Contains(ui))
            pauseUIs.Add(ui);
    }

    // Check if game should be paused or resumed
    public void CheckPauseState()
    {
        bool shouldPause = false;

        foreach (var ui in pauseUIs)
        {
            if (ui.activeSelf) 
            {
                shouldPause = true;
                break;
            }
        }

        Time.timeScale = shouldPause ? 0f : 1f;
    }
}