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
    }

    private void OnRucksackDroppedIn()
    {
        throw new NotImplementedException();
    }

    private void OnRucksakHowered(bool howered)
    {
        if(rucksackItemsManager.DraggedItem != null)
        {
            var settings = applicationSettings.GetRucksackItemSettings(rucksackItemsManager.DraggedItem.SettingsId);

            rucksack.Equip(settings.ItemType, rucksackItemsManager.DraggedItem.gameObject);
            rucksackItemsManager.EquipDraggedItem();
        }
    }
}
