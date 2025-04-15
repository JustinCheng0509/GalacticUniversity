using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISelectableHoverHandler : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D hoverCursor;                 // Set this to your hand cursor
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    private bool isCursorOverButton = false;

    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    void Start()
    {
        raycaster = FindAnyObjectByType<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        if (raycaster == null)
            Debug.LogError("GlobalCursorHoverHandler: No GraphicRaycaster found in scene!");
        if (eventSystem == null)
            Debug.LogError("GlobalCursorHoverHandler: No EventSystem found in scene!");
    }

    void Update()
    {
        if (raycaster == null || eventSystem == null) return;

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

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

        if (hoveringButton && !isCursorOverButton)
        {
            Cursor.SetCursor(hoverCursor, hotspot, cursorMode);
            isCursorOverButton = true;
        }
        else if (!hoveringButton && isCursorOverButton)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            isCursorOverButton = false;
        }
    }
}
