using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverworldItemPrefabController : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private Button _itemButton;
    [SerializeField] private GameObject _itemCount;
    [SerializeField] private TMP_Text _itemCountText;

    void Start()
    {
        // Set width of _itemCount to 20% of the width of the parent object
        RectTransform itemCountRect = _itemCount.GetComponent<RectTransform>();
        RectTransform parentRect = _itemCount.transform.parent.GetComponent<RectTransform>();
        itemCountRect.sizeDelta = new Vector2(parentRect.rect.width * 0.25f, parentRect.rect.width * 0.25f);
    }

    public void SetItemInfo(Sprite itemSprite, UnityEngine.Events.UnityAction onClickAction)
    {
        _itemImage.sprite = itemSprite;
        _itemButton.onClick.AddListener(onClickAction);
        _itemCount.SetActive(false);
    }

    public void SetItemInfo(Sprite itemSprite, int itemCount, UnityEngine.Events.UnityAction onClickAction)
    {
        _itemImage.sprite = itemSprite;
        _itemButton.onClick.AddListener(onClickAction);
        _itemCount.SetActive(true);
        _itemCountText.text = itemCount.ToString();
    }
}
