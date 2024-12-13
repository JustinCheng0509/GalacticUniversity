using System.Collections;
using UnityEngine;

public class OverworldGameStart : MonoBehaviour
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
        fadeCanvas.alpha = 1.0f;
        fadeCanvas.gameObject.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicVolume = musicSource.volume;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
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
    }
}
