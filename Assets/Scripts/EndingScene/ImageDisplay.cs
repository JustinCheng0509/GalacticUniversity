using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageDisplay : MonoBehaviour, IPointerClickHandler
{
    [Header("Image Settings")]
    public Sprite[] images;
    
    private Image displayImage;
    private int currentIndex = 0;
    private bool hasCompleted = false;

    void Start()
    {
        // Get the image component
        displayImage = GetComponent<Image>();
        if (images.Length > 0)
            displayImage.sprite = images[currentIndex];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowNextImage();
    }

    private void ShowNextImage()
    {
        if (hasCompleted) return;
        if (images.Length == 0) return;

        currentIndex++;

        if (currentIndex >= images.Length)
        {
            // We've reached the end of the images
            hasCompleted = true;
            Debug.Log("Reached the end of images.");
            return;
        }

        displayImage.sprite = images[currentIndex];
    }
}