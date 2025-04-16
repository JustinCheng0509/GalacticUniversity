using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    private float _spawnDelay = MinigameConstants.MINIGAME_ENEMY_BASE_SPAWN_DELAY;
    private float _minScale = MinigameConstants.MINIGAME_ENEMY_BASE_MIN_SCALE;
    private float _maxScale = MinigameConstants.MINIGAME_ENEMY_BASE_MAX_SCALE;
    private float _minMoveSpeed = MinigameConstants.MINIGAME_ENEMY_BASE_MIN_SPEED;
    private float _maxMoveSpeed = MinigameConstants.MINIGAME_ENEMY_BASE_MAX_SPEED;

    // Offset to spawn the asteroids above the screen
    private float _spawnYOffset = 2f;

    private GameDataManager _gameDataManager;

    void Start()
    {
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        UpdateDifficulty();
        _gameDataManager.OnDayUpdated += _ => UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        // Adjust spawn rate based on day
        _spawnDelay -= MinigameConstants.MINIGAME_ENEMY_DAILY_SPAWN_DELAY_DECREASE * (_gameDataManager.CurrentDay - 1);
        // Adjust scale based on day
        _maxScale += MinigameConstants.MINIGAME_ENEMY_DAILY_MAX_SCALE_INCREASE * (_gameDataManager.CurrentDay - 1);
        InvokeRepeating(nameof(SpawnAsteroid), 1f, _spawnDelay);
    }

    void SpawnAsteroid()
    {
        // Get the Y position of the top of the screen + padding
        float yPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + _spawnYOffset;
        // Get the X position within the width of the screen
        float xPos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x);
        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

        // Original movement direction
        Vector2 moveDirection = Vector2.down;

        // If spawn position is on the left side of the screen, add a random X direction to the right
        if (xPos < 0)
        {
            moveDirection = new Vector2(Random.Range(0f, 0.6f), moveDirection.y);
        } else if (xPos > 0)
        {
            moveDirection = new Vector2(Random.Range(-0.6f, 0f), moveDirection.y);
        }
        // Normalize the movement direction
        moveDirection.Normalize();

        // Randomize the scale of the asteroid
        float scale = Random.Range(_minScale, _maxScale);

        GameObject asteroid = Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);

        // Get the asteroid's script component
        AsteroidBehavior asteroidBehavior = asteroid.GetComponent<AsteroidBehavior>();

        asteroidBehavior.SetAttributes(scale, _minMoveSpeed, _maxMoveSpeed, moveDirection);
    }
}
