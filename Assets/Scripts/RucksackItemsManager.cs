using System;
using System.Collections.Generic;
using UnityEngine;

public class RucksackItemsManager
{
    struct ItemsCreationData
    {
        public int InstanceId;
        public string SettingsId;
        public Vector3 Position;

        public ItemsCreationData(int instanceId, string settingsId, Vector3 position)
        {
            InstanceId = instanceId;
            SettingsId = settingsId;
            Position = position;
        }
    }

    const int ItemsCountPerType = 3;

    public RucksackItemObject DraggedItem { get; private set; }

    ApplicationSettings applicationSettings = null;
    DataModel dataModel = null;
    GameObject rucksackItemsParent = null;
    ItemsCreationData[] itemsData = null;
    Dictionary<int, RucksackItemObject> rucksackItems = new Dictionary<int, RucksackItemObject>();

    public RucksackItemsManager(ApplicationSettings applicationSettings, DataModel dataModel)
    {
        this.applicationSettings = applicationSettings;
        this.dataModel = dataModel;

        rucksackItemsParent = new GameObject("ItemsParent");
    }

    public void LoadItemsData()
    {
        int instanceId = 1;

        RucksackItemType[] itemTypes = (RucksackItemType[])Enum.GetValues(typeof(RucksackItemType));

        itemsData = new ItemsCreationData[itemTypes.Length * ItemsCountPerType];
        for (int i = 0; i < itemTypes.Length; i++ )
        {
            for (int j = 0; j < ItemsCountPerType; j++)
            {
                string settingsId = applicationSettings.GetRandomRucksackItem(itemTypes[i]);

                itemsData[i * ItemsCountPerType + j] = new ItemsCreationData(instanceId++, settingsId, GetRandomPos());
            }
        }
    }

    Vector3 GetRandomPos()
    {
        float x = UnityEngine.Random.Range(-5, 5);
        float y = 1;
        float z = UnityEngine.Random.Range(-5, 5);
        return  new Vector3(x, y, z);
    }

    public void CreateItems()
    {
        foreach (var d in itemsData)
        {
            var settings = applicationSettings.GetRucksackItemSettings(d.SettingsId);
            if(settings == null)
            {
                Debug.LogError("item settings is null");
                continue;
            }

            var itemObject  = GameObject.Instantiate(settings.Prefab, d.Position , Quaternion.identity, rucksackItemsParent.transform);
            var rucksackItem = itemObject.GetComponent<RucksackItemObject>();

            rucksackItem.Init(d.InstanceId, d.SettingsId);
            rucksackItems.Add(d.InstanceId, rucksackItem);

            rucksackItem.DragStarted += OnDragStarted;
        }
    }

    void OnDragStarted(int itemId, bool started)
    {
        DraggedItem = started ? rucksackItems[itemId] : null;
    }

    public void EquipDraggedItem()
    {
        var settings = applicationSettings.GetRucksackItemSettings(DraggedItem.SettingsId);

        int slotIndex = 0;
        for(int i = 0; i < dataModel.RucksackData.Length; i++)
        {
            if (dataModel.RucksackData[i].ItemType == settings.ItemType)
            {
                slotIndex = i;
            }
        }

        var data = dataModel.RucksackData[slotIndex];

        if (data.ItemId.Value != Constants.UnEquippedItemId)
        {
            var oldItem = rucksackItems[data.ItemId.Value];
            oldItem.transform.SetParent(rucksackItemsParent.transform);
            oldItem.transform.position = GetRandomPos();
            oldItem.SetEquipped(false);
        }

        data.ItemId.Value = DraggedItem.InstanceId;
        DraggedItem.SetEquipped(true);
    }
}
