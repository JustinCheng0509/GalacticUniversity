using UnityEngine;
using UnityEngine.UI;

public class OverworldItemPrefabController : MonoBehaviour
{
    private Image _itemImage;
    private Button _itemButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemImage = GetComponent<Image>();
        _itemButton = GetComponent<Button>();
    }

    public void SetItemInfo(Sprite itemSprite, UnityEngine.Events.UnityAction onClickAction)
    {
        _itemImage.sprite = itemSprite;
        _itemButton.onClick.AddListener(onClickAction);
    }
}
