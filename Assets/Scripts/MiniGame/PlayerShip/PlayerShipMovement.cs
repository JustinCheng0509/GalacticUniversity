using UnityEngine;

public class PlayerShipMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Vector2 screenBounds;

    private float xPadding = 2f;
    private float yPadding = 3f;
    
    private Rigidbody2D rb;

    private float minSpeedFactor = 0.08f;
    private float maxSpeedFactor = 0.8f;

    [SerializeField] private PlayerShipInfo playerShipInfo;

    public bool canMove = false;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // Check if applying skill system
        if (!StaticValues.USE_SKILL_SYSTEM)
        {
            transform.position += new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        } else {
            // Calculate acceleration/deceleration based on maneuverability
            // float speedFactor = playerShipInfo.maneuverability/ 100f;
            float speedFactor = Mathf.Lerp(minSpeedFactor, maxSpeedFactor, playerShipInfo.gameData.maneuverability / 100f);

            // Calculate the desired velocity (direction multiplied by speed)
            Vector2 desiredVelocity = new Vector2(horizontalInput, Input.GetAxis("Vertical")) * speed;

            // Apply smooth acceleration/deceleration based on maneuverability
            // Apply velocity change over time to create deceleration
            Vector2 currentVelocity = rb.linearVelocity;
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
            rb.linearVelocity = velocityChange;
        }

        // Clamp player position to screen bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, screenBounds.x * -1 + xPadding, screenBounds.x - xPadding);
        pos.y = Mathf.Clamp(pos.y, screenBounds.y * -1 + yPadding, screenBounds.y - yPadding);
        transform.position = pos;
        
    }
}
