using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldInteractionNPCChat : MonoBehaviour
{
    [SerializeField] private OverworldPlayerStatusController _overworldPlayerStatusController;
    [SerializeField] private AudioSource _interactionAudioSource;
    [SerializeField] private List<AudioClip> _sfxNPCChatClips;

    private bool _isChatting = false;
    private Coroutine _chattingCoroutine;

    void Start()
    {
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _overworldPlayerStatusController.OnStatusChanged += HandleStatusChanged;
    }

    private void HandleStatusChanged(OverworldPlayerStatus status)
    {
        if (status == OverworldPlayerStatus.Chatting && !_isChatting)
        {
            _isChatting = true;
            _chattingCoroutine = StartCoroutine(PlayRandomChatClips());
        }
        else if (status != OverworldPlayerStatus.Chatting && _isChatting)
        {
            _isChatting = false;
            if (_chattingCoroutine != null)
            {
                StopCoroutine(_chattingCoroutine);
            }
            _interactionAudioSource.Stop();
            _interactionAudioSource.clip = null;
        }
    }

    private IEnumerator PlayRandomChatClips()
    {
        while (_isChatting && _sfxNPCChatClips.Count > 0)
        {
            AudioClip randomClip = _sfxNPCChatClips[Random.Range(0, _sfxNPCChatClips.Count)];
            _interactionAudioSource.clip = randomClip;
            _interactionAudioSource.loop = false;
            _interactionAudioSource.Play();

            yield return new WaitForSeconds(randomClip.length);
        }
    }
}
