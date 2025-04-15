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

        Sprite image = images[currentIndex];

        displayImage.sprite = image;
        
        // Scale the gameobject to the ratio of the image
        if (image != null)
        {
            float imageWidth = image.rect.width;
            float imageHeight = image.rect.height;
            float aspectRatio = imageWidth / imageHeight;

            // Get the screen size in canvas units
            Canvas canvas = displayImage.canvas;
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            float maxWidth = canvasRect.rect.width;
            float maxHeight = canvasRect.rect.height;

            float targetWidth = maxWidth;
            float targetHeight = targetWidth / aspectRatio;

            // If height goes beyond limit, scale based on height instead
            if (targetHeight > maxHeight)
            {
                targetHeight = maxHeight;
                targetWidth = targetHeight * aspectRatio;
            }

            RectTransform rt = displayImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(targetWidth, targetHeight);
        }
    }
}