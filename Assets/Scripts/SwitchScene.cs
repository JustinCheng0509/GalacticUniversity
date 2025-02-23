using System;
using System.Collections;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeCanvas;

    private AudioSource _musicSource;

    private float _targetVolume = 0.5f;

    public event Action OnFadeInCompleted;

    // [SerializeField]
    // private DialogController dialogController;

    // public GameObject gameEndPanel;

    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        // Find the BGM_AUDIO_SOURCE in the scene
        _musicSource = GameObject.Find(GameConstants.BGM_AUDIO_SOURCE).GetComponent<AudioSource>();
        _targetVolume = _musicSource.volume;

        _fadeCanvas.alpha = 1.0f;
        _fadeCanvas.gameObject.SetActive(true);
    }

    public void FadeInScene(float fadeDuration = 1f)
    {
        StartCoroutine(FadeInSceneCoroutine(fadeDuration));
    }

    IEnumerator FadeInSceneCoroutine(float fadeDuration)
    {
        _musicSource.volume = 0.0f;
        _musicSource.Play();
        // Fade in the music and fade out the black screen over fadeDuration seconds
        float time = 0.0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            _fadeCanvas.alpha = 1.0f - time / fadeDuration;
            _musicSource.volume = time / fadeDuration * _targetVolume;
            yield return null;
        }
        _fadeCanvas.alpha = 0.0f;
        _fadeCanvas.gameObject.SetActive(false);
        // check PlayerPrefs to see if the the intro dialogs have been played
        // if (!PlayerPrefs.HasKey("introDialogsPlayed"))
        // {
        //     dialogController.SetCurrentDialogs(dialogController.introDialogs);
        //     PlayerPrefs.SetInt("introDialogsPlayed", 1);
        // }
        OnFadeInCompleted?.Invoke();
    }

    public void FadeOutScene(string sceneName)
    {
        StartCoroutine(FadeOutSceneCoroutine(sceneName));
    }

    IEnumerator FadeOutSceneCoroutine(string sceneName, float fadeDuration = 1f)
    {
        _fadeCanvas.alpha = 0.0f;
        _fadeCanvas.gameObject.SetActive(true);
        // Fade out the music and fade in the black screen over fadeDuration seconds
        float time = 0.0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;
            _fadeCanvas.alpha = time / fadeDuration;
            _musicSource.volume = (1.0f - time / fadeDuration) * _targetVolume;
            yield return null;
        }
        _fadeCanvas.alpha = 1.0f;
        // _fadeCanvas.gameObject.SetActive(false);
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
        FadeOutScene(GameConstants.SCENE_MAIN_MENU);
    }
}
