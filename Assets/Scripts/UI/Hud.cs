using System;
using UnityEngine;

public class Hud : MonoBehaviour
{
    public RucksackMenu RucksackMenu { get { return rucksackMenu; } }

    [SerializeField] RucksackMenu rucksackMenu = null;

    ApplicationSettings applicationSettings = null;
    DataModel dataModel = null;
    RucksackItemsManager rucksackItemsManager = null;

    public void Init(ApplicationSettings applicationSettings, DataModel dataModel, RucksackItemsManager rucksackItemsManager)
    {
        this.applicationSettings = applicationSettings;
        this.dataModel = dataModel;
        this.rucksackItemsManager = rucksackItemsManager;

        for (int i = 0; i < dataModel.RucksackData.Length; i++)
        {
            SetRucksackItem(dataModel.RucksackData[i].ItemType);
        }

        dataModel.RucksackEquipmentChanged += OnRucksackEquipmentChanged;
        rucksackMenu.gameObject.SetActive(false);
    }

    void SetRucksackItem(RucksackItemType type)
    {
        int istanceId = dataModel.RucksackData[(int)type].ItemId;
        Sprite icon = null;

        if (istanceId != Constants.UnEquippedItemId)
        {
            var item = rucksackItemsManager.RucksackItems[istanceId];
            var settings = applicationSettings.GetRucksackItemSettings(item.SettingsId);
            icon = settings.Icon;
        }

        rucksackMenu.SetItemIcon((int)type, istanceId, icon);
    }

    void OnRucksackEquipmentChanged(RucksackItemType type, int instanceId)
    {
        SetRucksackItem(type);
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
