using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform healthBarRect;

    private float originalWidth;

    void Start()
    {
        healthBarRect = GetComponent<RectTransform>();
        originalWidth = healthBarRect.sizeDelta.x;
    }

    public void SetHealth(float healthPercent)
    {
        healthBarRect.sizeDelta = new Vector2(originalWidth * healthPercent, healthBarRect.sizeDelta.y);
    }
}