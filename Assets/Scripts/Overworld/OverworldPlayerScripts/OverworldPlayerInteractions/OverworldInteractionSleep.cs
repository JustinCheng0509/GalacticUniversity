using UnityEngine;

public class OverworldInteractionSleep : MonoBehaviour
{
    [SerializeField] private OverworldPlayerStatusController _overworldPlayerStatusController;
    [SerializeField] private AudioSource _interactionAudioSource;
    [SerializeField] private AudioClip _sfxSleepClip;

    private bool _isSleeping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnStatusChanged += HandleStatusChanged;
    }

    private void HandleStatusChanged(OverworldPlayerStatus status)
    {
        if (status == OverworldPlayerStatus.Sleeping && !_isSleeping)
        {
            _isSleeping = true;
            _interactionAudioSource.clip = _sfxSleepClip;
            _interactionAudioSource.loop = true;
            _interactionAudioSource.Play();
        } else if (status != OverworldPlayerStatus.Sleeping && _isSleeping)
        {
            _isSleeping = false;
            _interactionAudioSource.Stop();
            _interactionAudioSource.loop = false;
            _interactionAudioSource.clip = null;
        }
    }
}
