using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Hunger,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType ConsumableType;
    public float effectValue;
}

[CreateAssetMenu(fileName ="New Item",menuName ="New Item SO", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;

}
