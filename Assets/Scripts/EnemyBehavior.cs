using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the enemy moves downward
    public float rotationSpeed = 100f; // Speed of rotation in degrees per second
    public int health = 10; // Number of bullets required to destroy the enemy

    void Update()
    {
        // Move the enemy downward (toward the bottom of the screen)
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the enemy around its Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Destroy the enemy if it goes off the bottom of the screen
        if (transform.position.y < -6f) // Adjust this value based on your screen boundaries
        {
            Destroy(gameObject);
        }
    }
       void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision object is tagged as "Bullet"
        if (collision.CompareTag("Bullet"))
        {
            // Decrease health by 1
            health--;

            // Destroy the bullet
            Destroy(collision.gameObject);

            // Check if health reaches zero
            if (health <= 0)
            {
                Destroy(gameObject); // Destroy the enemy
            }
        }

                if (collision.CompareTag("Player"))
        {
            // Destroy the player
            Destroy(collision.gameObject);

            // Optionally, handle game over logic here
            Debug.Log("Player destroyed! Game Over.");
        }
    }
}