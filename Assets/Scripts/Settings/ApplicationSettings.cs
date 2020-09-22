using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ApplicationSettings", menuName = "ScriptableObjects/ApplicationSettings", order = 1)]

public class ApplicationSettings : ScriptableObject
{
    [SerializeField] RucksackItemSettings[] rucksackItems = null;

    public RucksackItemSettings[] RucksackItems { get { return rucksackItems; } }

    [Serializable]
    public class RucksackItemSettings
    {
        [SerializeField] string settingsId = string.Empty;
        [SerializeField] RucksackItemType itemType = RucksackItemType.Cube;
        [SerializeField] string name = string.Empty;
        [SerializeField] float weight = 1f;
        [SerializeField] GameObject prefab = null;
        [SerializeField] Texture2D icon = null;

        public string SettingsId { get { return settingsId; } }
        public RucksackItemType ItemType { get { return itemType; } }
        public string Name { get { return name; } }
        public float Weight { get { return weight; } }
        public GameObject Prefab { get { return prefab; } }
        public Texture2D Icon { get { return icon; } }
    }

    public string GetRandomRucksackItem(RucksackItemType itemType)
    {
        List<string> ids = new List<string>();
        for (int i = 0; i < rucksackItems.Length; i++)
        {
            if (rucksackItems[i].ItemType == itemType)
            {
                ids.Add(rucksackItems[i].SettingsId);
            }
        }

        if (ids.Count == 0)
        {
            Debug.LogError("Cant find item with type " + itemType);
            return null;
        }

        return ids[UnityEngine.Random.Range(0, ids.Count)];
    }

    public RucksackItemSettings GetRucksackItemSettings(string settingsId)
    {
        foreach(var r in rucksackItems)
        {
            if(r.SettingsId == settingsId)
            {
                return r;
            }
        }

        Debug.LogError("Cant find RucksackItemSettings with id: " + settingsId);
        return null;
    }

}
