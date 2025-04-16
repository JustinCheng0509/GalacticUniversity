using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectableHoverHandler : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D hoverCursor;

    private Vector2 _hotspot = Vector2.zero;
    private CursorMode _cursorMode = CursorMode.Auto;
    private bool _isCursorOverButton = false;
    private GraphicRaycaster[] _allRaycasters;
    private EventSystem _eventSystem;

    void Start()
    {
        _allRaycasters = FindObjectsByType<GraphicRaycaster>(FindObjectsSortMode.None);
        _eventSystem = EventSystem.current;

        if (_allRaycasters == null)
            Debug.LogError("GlobalCursorHoverHandler: No GraphicRaycaster found in scene!");
        if (_eventSystem == null)
            Debug.LogError("GlobalCursorHoverHandler: No EventSystem found in scene!");
    }

    void Update()
    {
        if (_allRaycasters == null || _allRaycasters.Length == 0 || _eventSystem == null) return;

        PointerEventData pointerData = new PointerEventData(_eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();

        // Raycast against all raycasters in the scene
        foreach (var raycaster in _allRaycasters) {
            if (raycaster.gameObject.activeInHierarchy) {
                // Use this one for raycasting
                raycaster.Raycast(pointerData, results);
            }
        }

        bool hoveringButton = false;

        foreach (var result in results)
        {
            // Debug.Log($"Result: {result.gameObject.name}");
            if (result.gameObject.GetComponent<Button>())
            {
                hoveringButton = true;
                break;
            }
        }

        if (hoveringButton && !_isCursorOverButton)
        {
            Cursor.SetCursor(hoverCursor, _hotspot, _cursorMode);
            _isCursorOverButton = true;
        }
        else if (!hoveringButton && _isCursorOverButton)
        {
            Cursor.SetCursor(null, Vector2.zero, _cursorMode);
            _isCursorOverButton = false;
        }
    }
}
