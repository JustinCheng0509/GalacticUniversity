using UnityEngine;
using UnityEngine.UI;

public class OverworldUILayoutListener : MonoBehaviour
{
    private OverworldUILayoutController _layoutController;

    void Start()
    {
        _layoutController = FindAnyObjectByType<OverworldUILayoutController>();
        _layoutController.OnLayoutRebuild += HandleLayoutRebuild;
    }

    void OnEnable()
    {
        if (_layoutController != null)
        {
            _layoutController.OnLayoutRebuild += HandleLayoutRebuild;
        }
    }

    void OnDisable()
    {
        if (_layoutController != null)
        {
            _layoutController.OnLayoutRebuild -= HandleLayoutRebuild;
        }
    }

    void OnDestroy()
    {
        if (_layoutController != null)
        {
            _layoutController.OnLayoutRebuild -= HandleLayoutRebuild;
        }
    }

    private void HandleLayoutRebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
