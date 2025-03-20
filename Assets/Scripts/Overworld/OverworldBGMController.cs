using System.Collections;
using UnityEngine;

public class OverworldBGMController : MonoBehaviour
{
    private GameDataManager _gameDataManager;
    private AudioSource _audioSource;
    
    [SerializeField] private AudioClip _dayTheme;
    [SerializeField] private AudioClip _nightTheme;
    [SerializeField] private AudioClip _dayToNightTransitionClip;  
    [SerializeField] private AudioClip _nightToDayTransitionClip;  

    private float targetVolume = 1.0f;
    private bool isDayTime = true;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        isDayTime = CheckDayTime();
        _audioSource = GetComponent<AudioSource>();
        targetVolume = _audioSource.volume;
        _audioSource.clip = isDayTime ? _dayTheme : _nightTheme;
        // _audioSource.Play();
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

        // Only switch BGM if time of day actually changes
        if (isNowDayTime != isDayTime)
        {
            AudioClip newBGM = isNowDayTime ? _dayTheme : _nightTheme;
            AudioClip transitionClip = isNowDayTime ? _nightToDayTransitionClip : _dayToNightTransitionClip;
            
            isDayTime = isNowDayTime; // Update state
            StartCoroutine(SwitchBGM(newBGM, transitionClip));
        }
    }

    private IEnumerator SwitchBGM(AudioClip newBGM, AudioClip transitionClip)
    {
        // Fade out current BGM
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.unscaledDeltaTime / 1; // fade out
            yield return null;
        }
        _audioSource.Stop();

        // Play transition clip
        if (transitionClip != null)
        {
            _audioSource.clip = transitionClip;
            _audioSource.volume = targetVolume; 
            _audioSource.Play();
            yield return new WaitForSeconds(transitionClip.length); // Wait until the transition clip is finished
        }

        // Switch to the new BGM and fade it in
        _audioSource.clip = newBGM;
        _audioSource.Play();
        _audioSource.volume = 0; 
        while (_audioSource.volume < targetVolume)
        {
            _audioSource.volume += Time.unscaledDeltaTime / 1; //fade 
            yield return null;
        }
    }
}
