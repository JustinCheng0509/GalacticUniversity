using UnityEngine;
using UnityEngine.InputSystem;

public class MinigamePlayerShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private float bulletSpeed = 10f;

    [SerializeField] private float fireRate = 0.2f;

    private float nextFireTime;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip shootSFX;

    private bool _canFire = false;
    private float _damage = 20f;

    private MinigameController _minigameController;
    private GameDataManager _gameDataManager;
    private MinigamePlayerHealthController _playerHealthController;

    public float Damage => _damage;

    void Start()
    {
        _minigameController = FindAnyObjectByType<MinigameController>();
        _minigameController.OnMinigameStart += () => _canFire = true;
        _minigameController.OnMinigamePause += () => _canFire = false;
        _minigameController.OnMinigameResume += () => _canFire = true;
        _minigameController.OnMinigameEnd += () => _canFire = false;

        _playerHealthController = FindAnyObjectByType<MinigamePlayerHealthController>();
        _playerHealthController.OnPlayerDeath += () => _canFire = false;
        _playerHealthController.OnPlayerRespawn += () => _canFire = true;

        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += () => UpdateDamage();
    }

    void Update()
    {
        // Debug.Log($"Can Fire: {_canFire}");
        if (!_canFire) return;

        float shootValue = _shootAction.action.ReadValue<float>();
        // Debug.Log($"Shoot Value: {shootValue}");
        if (shootValue > 0.5f && Time.time > nextFireTime)
        {
            // Debug.Log("Shooting");
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void UpdateDamage()
    {
        // Update only once when the game data is loaded
        _gameDataManager.OnGameDataLoaded -= UpdateDamage;
        _damage *= Mathf.Lerp(1f, 3f, _gameDataManager.Destruction / 100f);
        fireRate -= fireRate * (_gameDataManager.InventoryManager.ShipFireRateIncrease / 100f);
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * bulletSpeed;

        sfxSource.PlayOneShot(shootSFX);
    }
}
