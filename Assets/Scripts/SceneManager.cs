using UnityEngine;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    [System.Serializable]
    public class EquipEvent : UnityEvent<RucksackItemType, int, bool> { }

    public EquipEvent RucksackItemEquipped;

    [SerializeField] ApplicationSettings applicationSettings = null;
    [SerializeField] Rucksack rucksack = null;
    [SerializeField] NetworkManager networkManager = null;
    [SerializeField] DataModel dataModel = null;
    [SerializeField] Hud hud = null;

    RucksackItemsManager rucksackItemsManager = null;

    private void Start()
    {
        dataModel.Init(true);

        rucksackItemsManager = new RucksackItemsManager(applicationSettings, dataModel);
        rucksackItemsManager.LoadItemsData();
        rucksackItemsManager.CreateItems();

        rucksack.InventoryPressedEvent += OnInventoryPressedEvent;
        rucksack.DroppedIn += OnRucksackDroppedIn;

        hud.Init(applicationSettings, dataModel, rucksackItemsManager);
        hud.RucksackMenu.ItemSelected += OnRucksackMenuItemSelected;

        dataModel.RucksackEquipmentChanged += OnRucksackEquipmentChanged;
    }

    void OnRucksackEquipmentChanged(RucksackItemType type,  int instanceId, bool equipped)
    {
        networkManager.SendEquipEvent(instanceId, equipped);

        RucksackItemEquipped.Invoke(type, instanceId, equipped);
    }

    void OnRucksackMenuItemSelected(int itemId)
    {
        if (itemId != Constants.UnEquippedItemId)
        {
            rucksackItemsManager.UnEquip(itemId);
        }
    }

    void OnInventoryPressedEvent(bool pressed)
    {
        hud.ShowRucksackMenu(pressed && !rucksackItemsManager.IsDraggedObject);
    }

    void OnRucksackDroppedIn()
    {
        if (rucksackItemsManager.DraggedItem != null)
        {
            var itemSettings = applicationSettings.GetRucksackItemSettings(rucksackItemsManager.DraggedItem.SettingsId);
            rucksack.Equip(itemSettings.ItemType, rucksackItemsManager.DraggedItem.gameObject);

            rucksackItemsManager.EquipDraggedItem();
        }
    }
}
