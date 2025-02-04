using UnityEngine;
using UnityEngine.UI;

public class AsteroidBehavior : MonoBehaviour
{
    // To be changed after instantiation
    [Header("Adjustment Settings")]
    public float scale = 1f;
    public AudioSource sfxSource;
    public AudioClip enemyDestroyedSFX;
    public PlayerShipInfo playerShipInfo;
    public float minMoveSpeed = 3f;
    public float maxMoveSpeed = 8f;
    public Vector2 moveDirection = Vector2.down;

    // Movement
    private float rotationSpeed = 100f;
    private float moveSpeed = 6f;

    // Health and Damage
    private float maxHealth = 100;
    private float currentHealth;
    private float damage = 20;

    [SerializeField] private GameObject destructionEffect;

    void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        maxHealth *= scale;
        currentHealth = maxHealth;
        damage *= scale;
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Check if the enemy is out of the screen
        if (Camera.main.WorldToViewportPoint(transform.position).y < -2f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(playerShipInfo.damage);
            playerShipInfo.AddScore(playerShipInfo.damage);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            if (!playerShipInfo.isShieldActive) {
                playerShipInfo.TakeDamage(damage);
            }
            DestroyWithEffects();
        }
    }

    private void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
        {   
            DestroyWithEffects();
        }
    }

    private void DestroyWithEffects()
    {
        Instantiate(destructionEffect, transform.position, Quaternion.identity);
        // Change the scale of the destruction effect
        destructionEffect.transform.localScale = new Vector3(scale, scale, scale);
        sfxSource.PlayOneShot(enemyDestroyedSFX);
        Destroy(gameObject);
    }
}