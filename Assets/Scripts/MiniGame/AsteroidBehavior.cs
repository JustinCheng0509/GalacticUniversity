using UnityEngine;
using UnityEngine.UI;

public class AsteroidBehavior : MonoBehaviour
{
    private float _scale = 1f;
    private Vector2 _moveDirection = Vector2.down;

    // Movement
    private readonly float _rotationSpeed = 100f;
    private float _moveSpeed = 6f;

    // Health and Damage
    private float _maxHealth = 100;
    private float _currentHealth;
    private float _damage = 20;

    private float _score = GameConstants.MINIGAME_ENEMY_BASE_SCORE;

    [SerializeField] private GameObject _destructionEffect;
    [SerializeField] private AudioClip _enemyDestroyedSFX;
    
    private AudioSource _sfxSource;
    private MinigamePlayerShooting _playerShooting;
    private MinigameScoreController _minigameScoreController;
    private MinigamePlayerShieldController _playerShieldController;
    private MinigamePlayerHealthController _playerHealthController;
    private MinigameController _minigameController;

    private bool _canMove = false;

    void Start()
    {
        GameObject sfxSourceObject = GameObject.Find(GameConstants.ENEMY_DESTROYED_SFX_AUDIO_SOURCE);
        if (sfxSourceObject != null) {
            _sfxSource = sfxSourceObject.GetComponent<AudioSource>();
        }

        _playerShooting = FindAnyObjectByType<MinigamePlayerShooting>();
        _minigameScoreController = FindAnyObjectByType<MinigameScoreController>();
        _playerShieldController = FindAnyObjectByType<MinigamePlayerShieldController>();
        _playerHealthController = FindAnyObjectByType<MinigamePlayerHealthController>();

        _minigameController = FindAnyObjectByType<MinigameController>();
        _minigameController.OnMinigameEnd += () => _canMove = false;

        _currentHealth = _maxHealth;
    }

    public void SetAttributes(float scale, float minMoveSpeed, float maxMoveSpeed, Vector2 moveDirection)
    {
        _scale = scale;
        transform.localScale = new Vector3(scale, scale, scale);
        _maxHealth *= scale;
        _currentHealth = _maxHealth;
        _damage *= scale;
        _moveDirection = moveDirection;
        _moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        _score *= scale;
        _canMove = true;
    }

    void Update()
    {
        if (!_canMove) return;

        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);

        // Check if the enemy is out of the screen
        if (Camera.main.WorldToViewportPoint(transform.position).y < -2f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canMove) return;
        
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(_playerShooting.Damage);
            _minigameScoreController.Score += _playerShooting.Damage;
            _minigameScoreController.DamageDealt += _playerShooting.Damage;

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            if (!_playerShieldController.IsShieldActive) {
                _playerHealthController.TakeDamage(_damage);
            }
            DestroyWithEffects(false);
        }
    }

    private void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - damage);

        if (_currentHealth <= 0)
        {   
            DestroyWithEffects();
        }
    }

    private void DestroyWithEffects(bool isScored = true)
    {
        if (isScored) {
            // Add score to player
            _minigameScoreController.Score += _score;
            _minigameScoreController.DangersDestroyedScore += _score;
        }
        
        // Instantiate the destruction effect
        Instantiate(_destructionEffect, transform.position, Quaternion.identity);
        // Change the scale of the destruction effect
        _destructionEffect.transform.localScale = new Vector3(_scale, _scale, _scale);
        
        _sfxSource.PlayOneShot(_enemyDestroyedSFX);
        Destroy(gameObject);
    }
}