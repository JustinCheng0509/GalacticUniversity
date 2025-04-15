using UnityEngine;

public class MinigamePlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _minSpeedFactor = 0.25f;
    [SerializeField] private float _maxSpeedFactor = 0.9f;

    [Header("Screen Bounds Settings")]
    private Vector2 _screenBounds;
    [SerializeField] private float _xPadding = 2f;
    [SerializeField] private float _yPadding = 3f;
    
    private Rigidbody2D _rb;
    private bool _canMove = false;

    private MinigameController _minigameController;
    private GameDataManager _gameDataManager;
    private MinigamePlayerHealthController _playerHealthController;

    void Start()
    {
        _minigameController = FindAnyObjectByType<MinigameController>();
        _minigameController.OnMinigameStart += () => _canMove = true;
        _minigameController.OnMinigamePause += () => _canMove = false;
        _minigameController.OnMinigameResume += () => _canMove = true;
        _minigameController.OnMinigameEnd += () => _canMove = false;

        _playerHealthController = FindAnyObjectByType<MinigamePlayerHealthController>();
        _playerHealthController.OnPlayerDeath += () => _canMove = false;
        _playerHealthController.OnPlayerRespawn += () => _canMove = true;

        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        _canMove = true;        
    }

    void OnDisable()
    {
        _canMove = false;
    }

    void Update()
    {
        if (!_canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate acceleration/deceleration based on maneuverability
        float speedFactor = Mathf.Lerp(_minSpeedFactor, _maxSpeedFactor, _gameDataManager.Maneuverability / 100f);

        speedFactor += speedFactor * _gameDataManager.MinigameMoveSpeedBonus / 100f;

        // Calculate the desired velocity (direction multiplied by speed)
        Vector2 desiredVelocity = new Vector2(horizontalInput, Input.GetAxis("Vertical")) * _speed;

        // Apply smooth acceleration/deceleration based on maneuverability
        // Apply velocity change over time to create deceleration
        Vector2 currentVelocity = _rb.linearVelocity;
        Vector2 velocityChange;

        // Gradual deceleration if no input is given
        if (horizontalInput == 0 && verticalInput == 0)
        {
            // Apply gradual deceleration
            velocityChange = Vector2.Lerp(currentVelocity, Vector2.zero, speedFactor * Time.fixedDeltaTime);
        }
        else
        {
            // Apply smooth velocity change towards the desired velocity
            velocityChange = Vector2.Lerp(currentVelocity, desiredVelocity, speedFactor * Time.fixedDeltaTime);
        }

        // Set the velocity after applying smooth change
        _rb.linearVelocity = velocityChange;

        // Clamp player position to screen bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _screenBounds.x * -1 + _xPadding, _screenBounds.x - _xPadding);
        pos.y = Mathf.Clamp(pos.y, _screenBounds.y * -1 + _yPadding, _screenBounds.y - _yPadding);
        transform.position = pos;
    }
}
