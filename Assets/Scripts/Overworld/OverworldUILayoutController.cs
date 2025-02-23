using System;
using UnityEngine;

public class OverworldUILayoutController: MonoBehaviour
{
    public event Action OnLayoutRebuild;
    
    public void ForceLayoutRebuild()
    {
        OnLayoutRebuild?.Invoke();
    }
}
