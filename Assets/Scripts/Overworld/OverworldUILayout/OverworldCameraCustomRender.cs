using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class OverworldCameraCustomRender : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private List<Light2D> _lightsToDisable;

    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == _camera && _lightsToDisable != null && _lightsToDisable.Count > 0)
        {
            foreach (var light in _lightsToDisable)
            {
                light.enabled = false;
            }
        }
    }

    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera == _camera && _lightsToDisable != null && _lightsToDisable.Count > 0)
        {
            foreach (var light in _lightsToDisable)
            {
                light.enabled = true;
            }
        }
    }
}
