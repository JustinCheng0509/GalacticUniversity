using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "Scriptable Objects/Chest")]
public class Chest : ScriptableObject
{
    public string chestID;
    public List<ChestItemEntry> chestItems;
}

[System.Serializable]
public class ChestItemEntry
{
    public Item item;
    public int quantity = 1;
}