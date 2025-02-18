using System.Collections;
using UnityEngine;

public class MinigamePlayerShieldController : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    private bool _isShieldActive = false;

    public bool IsShieldActive => _isShieldActive;

    public void ActivateShield()
    {
        StartCoroutine(ActivateShieldCoroutine());
    }
    
    private IEnumerator ActivateShieldCoroutine()
    {
        shield.SetActive(true);
        _isShieldActive = true;
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
        _isShieldActive = false;
    }
}
