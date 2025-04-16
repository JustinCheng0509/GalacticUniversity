using UnityEngine;

public class MinigameUIPlayerShipSpriteController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite stationarySprite;
    [SerializeField] private Sprite movingSprite;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = stationarySprite;
    }

    public void UpdateSprite(bool isMoving)
    {
        if (isMoving)
        {
            _spriteRenderer.sprite = movingSprite;
        }
        else
        {
            _spriteRenderer.sprite = stationarySprite;
        }
    }
}
