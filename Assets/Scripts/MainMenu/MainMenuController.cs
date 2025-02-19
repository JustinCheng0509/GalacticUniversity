using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip startGameSFX;
    private SwitchScene _switchScene;

    void Start()
    {
        _switchScene = FindAnyObjectByType<SwitchScene>();
        _switchScene.FadeInScene();
    }

    public void StartGame(bool isNewGame) {
        if (isNewGame) {
            PlayerPrefs.DeleteAll();
            SavedDataManager.CreateNewGameData();
        }
        sfxAudioSource.PlayOneShot(startGameSFX);
        _switchScene.FadeOutScene(GameConstants.SCENE_OVERWORLD);
    }
}
