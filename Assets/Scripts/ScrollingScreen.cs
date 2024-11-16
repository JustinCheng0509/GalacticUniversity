using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject bg1; // First background object
    public GameObject bg2; // Second background object
    public float scrollSpeed = 2f; // Scrolling speed
    private float bgHeight; // Height of the background

    void Start()
    {
        // Calculate the height of the background based on the sprite bounds
        bgHeight = bg1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // Scroll both backgrounds downward
        bg1.transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);
        bg2.transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        // Check if bg1 is completely off-screen
        if (bg1.transform.position.y <= -bgHeight)
        {
            // Move bg1 to the top of bg2
            bg1.transform.position = new Vector3(bg1.transform.position.x, bg2.transform.position.y + bgHeight, bg1.transform.position.z);

            // Swap references
            SwapBackgrounds();
        }

        // Check if bg2 is completely off-screen
        if (bg2.transform.position.y <= -bgHeight)
        {
            // Move bg2 to the top of bg1
            bg2.transform.position = new Vector3(bg2.transform.position.x, bg1.transform.position.y + bgHeight, bg2.transform.position.z);

            // Swap references
            SwapBackgrounds();
        }
    }

    void SwapBackgrounds()
    {
        // Swap bg1 and bg2 references
        GameObject temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}
