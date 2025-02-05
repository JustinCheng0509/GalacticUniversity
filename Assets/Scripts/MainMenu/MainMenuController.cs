using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip startGameSFX;

    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 2f;

    [SerializeField] private GameDataManager gameDataManager;

    public void StartGame(bool isNewGame) {
        if (isNewGame) {
            PlayerPrefs.DeleteAll();
            gameDataManager.CreateNewGameData();
        }
        sfxAudioSource.PlayOneShot(startGameSFX);
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine() {
        float musicVolume = musicAudioSource.volume;
        fadeCanvas.alpha = 0;
        fadeCanvas.gameObject.SetActive(true);
        // Fade in the fadeCanvas and fade out the music over fadeDuration
        for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
            fadeCanvas.alpha = t / fadeDuration;
            musicAudioSource.volume = Mathf.Lerp(musicVolume, 0, t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 1;
        musicAudioSource.Stop();
        LoadGameScene();
    }

    private void LoadGameScene() {
        SceneManager.LoadScene(StaticValues.SCENE_OVERWORLD);
    }
}
