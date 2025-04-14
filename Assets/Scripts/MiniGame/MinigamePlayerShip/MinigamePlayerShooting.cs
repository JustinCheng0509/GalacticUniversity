using UnityEngine;

public class MinigamePlayerShooting : MonoBehaviour
{
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
        _gameDataManager.OnDestructionUpdated += _ => UpdateDamage();
        UpdateDamage();
    }

    void OnEnable()
    {
        _canFire = true;        
    }

    void OnDisable()
    {
        _canFire = false;
    }

    void Update()
    {
        if (!_canFire) return;

        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void UpdateDamage()
    {
        _damage *= Mathf.Lerp(1f, 3f, _gameDataManager.Destruction / 100f);
    }

    void Fire()
    {
        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * bulletSpeed;

        // Play shooting sound effect
        sfxSource.PlayOneShot(shootSFX);
    }

}
