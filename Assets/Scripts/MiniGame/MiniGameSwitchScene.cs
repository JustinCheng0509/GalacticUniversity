using System.Collections;
using UnityEngine;

public class MiniGameSwitchScene : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadeCanvas;

    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private float fadeDuration = 1.0f;

    private float musicVolume;


    // Awake is called once before the first execution of Start after the MonoBehaviour is created
    void Awake()
    {
        // fadeCanvas.alpha = 1.0f;
        // fadeCanvas.gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicVolume = musicSource.volume;
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
