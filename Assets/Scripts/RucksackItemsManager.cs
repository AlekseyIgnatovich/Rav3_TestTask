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

                float x = UnityEngine.Random.Range(-5, 5);
                float y = 1;
                float z = UnityEngine.Random.Range(-5, 5);
                Vector3 position = new Vector3(x, y, z);

                itemsData[i * ItemsCountPerType + j] = new ItemsCreationData(instanceId++, settingsId, position);
            }
        }
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
        }
    }
}
