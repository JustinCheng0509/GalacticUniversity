using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Scriptable Objects/Chest")]
public class Chest : ScriptableObject
{
    public string chestID;
    public List<Item> items;
}
