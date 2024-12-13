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

    private float musicVolume;

    [SerializeField]
    private DialogueController dialogueController;

    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        fadeCanvas.alpha = 1.0f;
        fadeCanvas.gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicVolume = musicSource.volume;
        StartCoroutine(FadeInGame());
    }

    IEnumerator FadeInGame()
    {
        musicSource.volume = 0.0f;
        musicSource.Play();
        // Fade in the music and fade out the black screen over fadeDuration seconds
        float time = 0.0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = 1.0f - time / fadeDuration;
            musicSource.volume = time / fadeDuration * musicVolume;
            yield return null;
        }
        fadeCanvas.alpha = 0.0f;
        fadeCanvas.gameObject.SetActive(false);
        // check PlayerPrefs to see if the the intro dialogues have been played
        if (!PlayerPrefs.HasKey("introDialoguesPlayed"))
        {
            dialogueController.SetCurrentDialogues(dialogueController.introDialogues);
            PlayerPrefs.SetInt("introDialoguesPlayed", 1);
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
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvas.alpha = time / fadeDuration;
            musicSource.volume = (1.0f - time / fadeDuration) * musicVolume;
            yield return null;
        }
        fadeCanvas.alpha = 1.0f;
        fadeCanvas.gameObject.SetActive(true);
        // Load the scene after the fade out is complete
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
