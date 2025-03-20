using UnityEngine;

public class UIPause : MonoBehaviour
{
    private void OnEnable()
    {
        PauseManager.Instance.RegisterPauseUI(gameObject);
        PauseManager.Instance.CheckPauseState();
    }

    private void OnDisable()
    {
        PauseManager.Instance.CheckPauseState();
    }
}
