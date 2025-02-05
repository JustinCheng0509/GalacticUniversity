using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 0.4f;

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip enemyDestroyedSFX;

    [SerializeField]
    private PlayerShipInfo playerShipInfo;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnRate);
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

        // Randomize the scale of the asteroid
        float scale = Random.Range(1f, 2f);

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Get the asteroid's script component
        AsteroidBehavior asteroid = asteroidPrefab.GetComponent<AsteroidBehavior>();

        // Set the asteroid's properties
        asteroid.scale = scale;
        asteroid.sfxSource = sfxSource;
        asteroid.enemyDestroyedSFX = enemyDestroyedSFX;
        asteroid.playerShipInfo = playerShipInfo;
        asteroid.moveDirection = moveDirection;
    }
}
