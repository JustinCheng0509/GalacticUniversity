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

    private void HandleLayoutRebuild()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
