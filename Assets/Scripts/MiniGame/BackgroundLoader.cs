using UnityEngine;
using System.Collections;

public class BackgroundLoader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(LoadBackgroundAsync());
    }

    IEnumerator LoadBackgroundAsync()
    {
        ResourceRequest request = Resources.LoadAsync<Sprite>("bg2");
        yield return request;

        if (request.asset != null)
        {
            spriteRenderer.sprite = (Sprite)request.asset;
            Debug.Log("Background loaded successfully!");
        }
        else
        {
            Debug.LogError("Failed to load background sprite!");
        }
    }
}
