using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
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

        rucksack.Init(rucksackItemsManager);
        rucksack.InventoryHovered += OnRucksakHowered;
        rucksack.DroppedIn += OnRucksackDroppedIn;

        hud.Init(applicationSettings, dataModel, rucksackItemsManager);
    }

    private void OnRucksackDroppedIn()
    {
        if (rucksackItemsManager.DraggedItem != null)
        {
            var itemSettings = applicationSettings.GetRucksackItemSettings(rucksackItemsManager.DraggedItem.SettingsId);
            rucksack.Equip(itemSettings.ItemType, rucksackItemsManager.DraggedItem.gameObject);

            rucksackItemsManager.EquipDraggedItem();
        }
    }

    private void OnRucksakHowered(bool howered)
    {
        if (howered && rucksackItemsManager.DraggedItem != null)
        {
            var settings = applicationSettings.GetRucksackItemSettings(rucksackItemsManager.DraggedItem.SettingsId);

            rucksack.Equip(settings.ItemType, rucksackItemsManager.DraggedItem.gameObject);
            rucksackItemsManager.EquipDraggedItem();

            return;
        }

        if (rucksackItemsManager.DraggedItem == null)
        {
            hud.ShowRucksackMenu(howered);
        }
    }
}
