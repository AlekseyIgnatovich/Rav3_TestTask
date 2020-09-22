using UnityEngine;

public static class Constants
{
    public const int UnEquippedItemId = -1;

    public static class Layers
    {
        public const int InventoryItemLayer = 8;
        public const int GroundLayer = 9;
        public const int RucksackdLayer = 10;

        public static readonly LayerMask InventoryItemMask = LayerMask.GetMask("InventoryItem");
        public static readonly LayerMask GroundMask = LayerMask.GetMask("Ground");
        public static readonly LayerMask RucksackdMask = LayerMask.GetMask("Rucksack");
    }
}

public enum RucksackItemType
{
    Sphere,
    Cube,
    Cylinder,
}
public enum EquipEventType
{
    Equipped,
    UnEquipped
}