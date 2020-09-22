using System;
using UnityEngine;

using static Constants;

[CreateAssetMenu(fileName = "DataModel", menuName = "ScriptableObjects/DataModel", order = 2)]
public class DataModel : ScriptableObject
{
    public event Action<RucksackItemType, int> RucksackEquipmentChanged;

    public RucksackSlotModel[] RucksackData { get { return rucksackData; } }

    [SerializeField] RucksackSlotModel[] rucksackData = null;

    [Serializable]
    public class RucksackSlotModel
    {
        public RucksackItemType ItemType;
        public NotifableInt ItemId;

        public RucksackSlotModel(RucksackItemType itemType, int itemId/*, bool equipped*/)
        {
            ItemType = itemType;
            ItemId = new NotifableInt();
            ItemId.Value = itemId;
        }
    }

    public void Init(bool clearModel = false)
    {
        Debug.Log("Init model");

        if(clearModel || rucksackData == null || rucksackData.Length != Enum.GetValues(typeof(RucksackItemType)).Length)
        {
            CreateModel();
        }
    }

    void CreateModel()
    {
        Debug.Log("Create fresh model");

        RucksackItemType[] itemValues = (RucksackItemType[])Enum.GetValues(typeof(RucksackItemType));
        rucksackData = new RucksackSlotModel[itemValues.Length];

        for (int i = 0; i < itemValues.Length; i ++)
        {
            rucksackData[i] = new RucksackSlotModel(itemValues[i], UnEquippedItemId);
            rucksackData[i].ItemId.Changed += OnSlotChanged;
        }
    }

    void OnSlotChanged(int index)
    {
        RucksackEquipmentChanged?.Invoke(rucksackData[index].ItemType, rucksackData[index].ItemId.Value);
    }
}
