using UnityEngine;
using UnityEngine.UI;

public class OverworldItemPrefabController : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _itemButton;

    public void SetItemInfo(Sprite itemSprite, UnityEngine.Events.UnityAction onClickAction)
    {
        _itemImage.sprite = itemSprite;
        _itemButton.onClick.AddListener(onClickAction);
    }
}
