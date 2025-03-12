using System.Collections;
using UnityEngine;

public class OverworldBGMController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dayTheme;
    [SerializeField] private AudioClip _nightTheme;
    [SerializeField] private AudioClip _dayToNightTransitionClip;  // Day to Night transition clip
    [SerializeField] private AudioClip _nightToDayTransitionClip;  // Night to Day transition clip

    private float targetVolume = 1.0f;
    private bool isDayTime = true;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        isDayTime = CheckDayTime();
        _audioSource = GetComponent<AudioSource>();
        targetVolume = _audioSource.volume;
        _audioSource.clip = isDayTime ? _dayTheme : _nightTheme;
        _audioSource.Play();
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
            AudioClip transitionClip = isDayTime ? _nightToDayTransitionClip : _dayToNightTransitionClip;
            StartCoroutine(SwitchBGM(newClip, transitionClip));
        }
    }

    private IEnumerator SwitchBGM(AudioClip newClip, AudioClip transitionClip)
    {
        // Fade out the current clip
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.unscaledDeltaTime / 5; //  fade out
            yield return null;
        }
        _audioSource.Stop();

        // Play the transition clip (with fade-in)
        _audioSource.clip = transitionClip;
        _audioSource.Play();
        while (_audioSource.volume < targetVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime / 5; // Fades in transition 
            yield return null;
        }

        // Stop transition audio and switch to new BGM (day or night)
        _audioSource.Stop();
        _audioSource.clip = newClip;
        _audioSource.Play();
        while (_audioSource.volume < targetVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime /5; // Fades in the new BGM 
            yield return null;
        }
    }
}
