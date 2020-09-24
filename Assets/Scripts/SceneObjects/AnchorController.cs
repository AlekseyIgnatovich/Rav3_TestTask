using System;
using UnityEngine;

using static Constants;

public class AnchorController : MonoBehaviour
{
    [Serializable]
    class AnchorPoint
    {
        public RucksackItemType ItemType = RucksackItemType.Cube;
        public Transform AnchorTransform = null;
    }

    [SerializeField] AnchorPoint[] anchors = null;

    public Transform GetAnchor(RucksackItemType ItemType)
    {
        for (int i = 0; i < anchors.Length; i++)
        {
            if (anchors[i].ItemType == ItemType)
            {
                return anchors[i].AnchorTransform;
            }
        }

        Debug.LogError("Cant find anchor " + ItemType.ToString() + " return default position");
        return transform;
    }
}
