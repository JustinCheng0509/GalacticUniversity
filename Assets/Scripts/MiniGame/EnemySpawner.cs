using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private float _spawnRate = 0.4f;
    [SerializeField] private float _maxScale = 2f;
    [SerializeField] private float _minMoveSpeed = 6f;
    [SerializeField] private float _maxMoveSpeed = 10f;

    private GameDataManager _gameDataManager;

    void Start()
    {
        _gameDataManager = FindFirstObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;

        InvokeRepeating(nameof(SpawnAsteroid), 1f, _spawnRate);
    }

    private void OnGameDataLoaded()
    {
        // Adjust spawn rate based on day
        _spawnRate -= 0.05f * (_gameDataManager.CurrentDay - 1);
        // Adjust scale based on day
        _maxScale += 0.4f * (_gameDataManager.CurrentDay - 1);
    }

    void SpawnAsteroid()
    {
        // Get the Y position of the top of the screen + padding
        float yPos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + 2f;
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
        float scale = Random.Range(1f, _maxScale);

        Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);

        // Get the asteroid's script component
        AsteroidBehavior asteroidBehavior = _asteroidPrefab.GetComponent<AsteroidBehavior>();

        asteroidBehavior.SetAttributes(scale, _minMoveSpeed, _maxMoveSpeed, moveDirection);
    }
}
