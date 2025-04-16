using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerNameInputField;
    [SerializeField] private Button _continueButton;
    private SwitchScene _switchScene;

    void Start()
    {
        _switchScene = FindAnyObjectByType<SwitchScene>();
        _switchScene.FadeInScene();
        
        if (SavedDataManager.GameDataExists())
        {
            _continueButton.interactable = true;
        }
        else
        {
            _continueButton.interactable = false;
            // Change opacity of the continue button to indicate it's not interactable
            Color color = _continueButton.image.color;
            color.a = 0.25f; // Set to 25% opacity
            _continueButton.image.color = color;

        }
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

    public void ContinueGame() {
        if (SavedDataManager.GameDataExists()) {
            GameData gameData = SavedDataManager.LoadGameData();
            if (gameData != null) {
                string currentScene = gameData.currentScene;
                if (currentScene == GameConstants.SCENE_OVERWORLD) {
                    _switchScene.FadeOutScene(GameConstants.SCENE_OVERWORLD);
                } else if (currentScene == GameConstants.SCENE_ENDING) {
                    _switchScene.FadeOutScene(GameConstants.SCENE_ENDING);
                } else if (currentScene == GameConstants.SCENE_MINIGAME)    {
                    _switchScene.FadeOutScene(GameConstants.SCENE_MINIGAME);
                } else {
                    Debug.LogError("Unknown scene: " + currentScene);
                }
            } else {
                Debug.LogError("Failed to load game data.");
            }
        }
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
