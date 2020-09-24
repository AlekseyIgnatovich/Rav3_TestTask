using UnityEngine;

public class Hud : MonoBehaviour
{
    public RucksackMenu RucksackMenu { get { return rucksackMenu; } }

    [SerializeField] RucksackMenu rucksackMenu = null;

    ApplicationSettings applicationSettings = null;
    RucksackItemsManager rucksackItemsManager = null;

    public void Init(ApplicationSettings applicationSettings, DataModel dataModel, RucksackItemsManager rucksackItemsManager)
    {
        this.applicationSettings = applicationSettings;
        this.rucksackItemsManager = rucksackItemsManager;

        for (int i = 0; i < dataModel.RucksackData.Length; i++)
        {
            SetRucksackItem(dataModel.RucksackData[i].ItemType, Constants.UnEquippedItemId, false);
        }

        dataModel.RucksackEquipmentChanged += OnRucksackEquipmentChanged;
        rucksackMenu.gameObject.SetActive(false);
    }

    void SetRucksackItem(RucksackItemType type, int istanceId, bool equipped)
    {
        Sprite icon = null;

        if (equipped && istanceId != Constants.UnEquippedItemId)
        {
            var item = rucksackItemsManager.RucksackItems[istanceId];
            var settings = applicationSettings.GetRucksackItemSettings(item.SettingsId);
            icon = settings.Icon;
        }

        rucksackMenu.SetItemIcon((int)type, istanceId, icon);
    }

    void OnRucksackEquipmentChanged(RucksackItemType type, int instanceId, bool equipped)
    {
        SetRucksackItem(type, instanceId, equipped);
    }

    public void ShowRucksackMenu(bool show)
    {
        if (show)
        {
            rucksackMenu.Show();
        }
        else
        {
            rucksackMenu.Hide();
        }
    }
}
