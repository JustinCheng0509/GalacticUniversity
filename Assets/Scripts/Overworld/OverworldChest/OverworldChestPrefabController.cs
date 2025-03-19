using UnityEngine;

public class OverworldChestPrefabController : MonoBehaviour
{
    [SerializeField] private Chest _chest;
    [SerializeField] private Sprite _closedChestSprite;
    [SerializeField] private Sprite _openedChestSprite;

    public Chest Chest { get => _chest; }

    private SpriteRenderer _spriteRenderer;
    private GameDataManager _gameDataManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameDataManager = FindAnyObjectByType<GameDataManager>();
        _gameDataManager.OnGameDataLoaded += OnGameDataLoaded;
        _gameDataManager.OnChestOpened += OnChestOpened;
    }

    private void OnGameDataLoaded()
    {
        if (_gameDataManager.IsChestOpened(_chest.chestID))
        {
            _spriteRenderer.sprite = _openedChestSprite;
            // Change layer to default to prevent player from interacting with it
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            _spriteRenderer.sprite = _closedChestSprite;
            gameObject.layer = LayerMask.NameToLayer(GameConstants.INTERACTABLE_LAYER);
        }
    }

    private void OnChestOpened(string chestID)
    {
        if (chestID == _chest.chestID)
        {
            _spriteRenderer.sprite = _openedChestSprite;
            // Change layer to default to prevent player from interacting with it
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
