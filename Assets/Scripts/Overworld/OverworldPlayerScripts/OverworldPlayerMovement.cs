using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class OverworldPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _moveDirection;

    [SerializeField] private InputActionReference _moveAction;

    [SerializeField] private AudioClip[] _footstepGrass;
    [SerializeField] private AudioClip[] _footstepConcrete;
    [SerializeField] private AudioClip[] _footstepIndoor;

    [SerializeField] private Transform _footstepPosition;

    [SerializeField] private GridLayout _gridLayout;

    [SerializeField] private Tilemap[] _groundTilemaps;

    [SerializeField] private AudioSource _footstepAudioSource;

    private bool _delayFootstep = false;

    private OverworldPlayerStatusController _overworldPlayerStatusController;
    private GameDataManager _gameDataManager;

    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _overworldPlayerStatusController = FindAnyObjectByType<OverworldPlayerStatusController>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
    }

    private void Update() {
        _moveDirection = _moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate() { 
        if (_moveDirection != Vector2.zero) {
            // moveY is reversed because of the sprite orientation
            _animator.SetInteger("moveY", _moveDirection.y > 0 ? -1 : _moveDirection.y < 0 ? 1 : 0);
            if (_moveDirection.y == 0) {
                _spriteRenderer.flipX = _moveDirection.x < 0;
            }
            _animator.SetInteger("moveX", _moveDirection.x > 0 ? 1 : _moveDirection.x < 0 ? -1 : 0);
            PlayFootstepSound();
            _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Walking;
        } else if (_overworldPlayerStatusController.CurrentStatus == OverworldPlayerStatus.Walking) {
            _overworldPlayerStatusController.CurrentStatus = OverworldPlayerStatus.Idle;
            _animator.SetInteger("moveX", 0);
            _animator.SetInteger("moveY", 0);
        }
        float moveSpeed = _moveSpeed;
        if (_gameDataManager != null) {
            moveSpeed = _moveSpeed + _moveSpeed * _gameDataManager.MoveSpeedBonus / 100;
        }
        _rb.linearVelocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * moveSpeed);    
    }

    private void PlayFootstepSound() {
        if (_delayFootstep) {
            return;
        }

        AudioClip[] footstepSounds = _footstepConcrete;

        string tileTag = GetTileTag(_footstepPosition.position);

        if (tileTag == GameConstants.TAG_GRASS) {
            footstepSounds = _footstepGrass;
        } else if (tileTag == GameConstants.TAG_INDOOR) {
            footstepSounds = _footstepIndoor;
        }
        
        _footstepAudioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        _footstepAudioSource.volume = Random.Range(0.7f, 1f);
        _footstepAudioSource.pitch = Random.Range(0.9f, 1.05f);
        _footstepAudioSource.Play();

        _delayFootstep = true;
        Invoke("ResetFootstepDelay", 0.3f);
    }

    private string GetTileTag(Vector2 footstepPosition) {
        Vector3Int cellPosition = _gridLayout.WorldToCell(footstepPosition);
        // Check each tilemap for the tile at the given position
        foreach (Tilemap tilemap in _groundTilemaps) {
            TileBase tile = tilemap.GetTile(cellPosition);
            if (tile != null) {
                return tilemap.tag;
            }
        }
        return null;
    }

    private void ResetFootstepDelay() {
        _delayFootstep = false;
    }
}
