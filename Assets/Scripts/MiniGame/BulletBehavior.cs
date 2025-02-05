using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the bullet is out of the screen
        if (Camera.main.WorldToViewportPoint(transform.position).y > 2f)
        {
            Destroy(gameObject);
        }
    }
}
