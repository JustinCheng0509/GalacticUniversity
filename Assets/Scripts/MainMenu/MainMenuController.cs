using System.Collections;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInputField;
    private SwitchScene _switchScene;

    void Start()
    {
        _switchScene = FindAnyObjectByType<SwitchScene>();
        _switchScene.FadeInScene();
    }

    public void StartGame(bool isNewGame) {
        if (isNewGame) {
            PlayerPrefs.DeleteAll();
            string playerName = _playerNameInputField.text;
            if (playerName == "") {
                playerName = "Player";
            }
            SavedDataManager.CreateNewGameData(playerName);
        }
        _switchScene.FadeOutScene(GameConstants.SCENE_OVERWORLD);
    }
}
