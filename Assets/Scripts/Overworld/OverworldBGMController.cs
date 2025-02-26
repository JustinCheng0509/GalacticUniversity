using System.Collections;
using UnityEngine;

public class OverworldBGMController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dayTheme;
    [SerializeField] private AudioClip _nightTheme;

    private float targetVolume = 1.0f;

    private bool isDayTime = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        isDayTime = CheckDayTime();
        _audioSource = GetComponent<AudioSource>();
        targetVolume = _audioSource.volume;
        _audioSource.clip = isDayTime ? _dayTheme : _nightTheme;
        _gameDataManager.OnTimeUpdated += UpdateBGM;
    }

    private bool CheckDayTime()
    {
        return OverworldTimeController.IsWithinTimeRange(
            _gameDataManager.CurrentTime,
            "05:30",
            "19:00"
        );
    }

    private void UpdateBGM(string time)
    {
        bool isNowDayTime = CheckDayTime();
        if (isNowDayTime != isDayTime)
        {
            isDayTime = isNowDayTime;
            AudioClip newClip = isDayTime ? _dayTheme : _nightTheme;
            StartCoroutine(SwitchBGM(newClip));
        }
    }

    private IEnumerator SwitchBGM(AudioClip newClip)
    {
        // Fade out the current clip
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.unscaledDeltaTime / 2; // 2 seconds to fade out
            yield return null;
        }
        _audioSource.Stop();
        _audioSource.clip = newClip;
        // Fade in the new clip
        _audioSource.Play();
        while (_audioSource.volume < targetVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime / 2; // 2 seconds to fade in
            yield return null;
        }
    }
}
