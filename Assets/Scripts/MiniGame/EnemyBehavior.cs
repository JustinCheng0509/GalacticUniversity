using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 100f;
    
    [Header("Health Settings")]
    public int maxHealth = 10;
    private int currentHealth;
    
    [Header("UI Elements")]
    public Image healthBarImage;  // Reference to your existing UI Image

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip enemyDestroyedSFX;
    
    private void OnEnable()
    {
        currentHealth = maxHealth;
        if (healthBarImage != null)
        {
            healthBarImage.gameObject.SetActive(true);
            healthBarImage.fillAmount = 1f;
        }
    }

    private void OnDisable()
    {
        if (healthBarImage != null)
        {
            healthBarImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            // Destroy(collision.gameObject);
            // Debug.Log("Player destroyed! Game Over.");
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {   
            sfxSource.PlayOneShot(enemyDestroyedSFX);
            Destroy(gameObject);
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}