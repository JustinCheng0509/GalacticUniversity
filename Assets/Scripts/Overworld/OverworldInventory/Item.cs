using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemValue;

    [Header("Consumable")]
    public bool isConsumable;
    public int energyRestore;
    public int hungerRestore;
    public int moodRestore;

    [Header("Passive Effects (Non-Consumable)")]
    public bool hasPassiveEffect;
    public float energyRecoveryBonus;
    public float overworldMoveSpeedBonus;
    public float minigameMoveSpeedBonus;
}
