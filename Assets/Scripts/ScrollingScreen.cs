using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Material backgroundMaterial; // Assign your unlit texture material here
    public float scrollSpeed = 0.2f; // Scrolling speed

    void Update()
    {
        backgroundMaterial.mainTextureOffset += new Vector2(0f, scrollSpeed * Time.deltaTime);
    }
}
