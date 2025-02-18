using System.Collections;
using UnityEngine;

public class OverworldSwitchScene : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadeCanvas;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private float fadeDuration = 1.0f;

    private float musicVolume = 0.5f;

    [SerializeField]
    private DialogController dialogController;

    public GameObject gameEndPanel;

    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        fadeCanvas.alpha = 1.0f;
        fadeCanvas.gameObject.SetActive(true);
    }
    public void FadeInGame()
    {
        StartCoroutine(FadeInGameCoroutine());
    }

    IEnumerator FadeInGameCoroutine()
    {
        musicSource.volume = 0.0f;
        musicSource.Play();
        // Fade in the music and fade out the black screen over fadeDuration seconds
        float time = 0.0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadeCanvas.alpha = 1.0f - time / fadeDuration;
            musicSource.volume = time / fadeDuration * musicVolume;
            yield return null;
        }
        fadeCanvas.alpha = 0.0f;
        fadeCanvas.gameObject.SetActive(false);
        // check PlayerPrefs to see if the the intro dialogs have been played
        if (!PlayerPrefs.HasKey("introDialogsPlayed"))
        {
            dialogController.SetCurrentDialogs(dialogController.introDialogs);
            PlayerPrefs.SetInt("introDialogsPlayed", 1);
        }
    }

    public void FadeOutGame(string sceneName)
    {
        StartCoroutine(FadeOutGameCoroutine(sceneName));
    }

    IEnumerator FadeOutGameCoroutine(string sceneName)
    {
        fadeCanvas.alpha = 0.0f;
        fadeCanvas.gameObject.SetActive(true);
        // Fade out the music and fade in the black screen over fadeDuration seconds
        float time = 0.0f;
        float fadeDuration = 3f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            fadeCanvas.alpha = time / fadeDuration;
            musicSource.volume = (1.0f - time / fadeDuration) * musicVolume;
            yield return null;
        }
        fadeCanvas.alpha = 1.0f;
        fadeCanvas.gameObject.SetActive(true);
        // Load the scene after the fade out is complete
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        FadeOutGame(GameConstants.SCENE_MAIN_MENU);
    }
}
