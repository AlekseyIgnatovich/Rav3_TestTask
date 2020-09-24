using System;
using UnityEngine;

public class Rucksack : MonoBehaviour
{
    public event Action<bool> InventoryPressedEvent = null;
    public event Action DroppedIn = null;

    public bool InventoryPressed { get; private set; }

    [SerializeField] AnchorController anchorController = null;

    bool howered = false;

    public void Equip(RucksackItemType type, GameObject item)
    {
        var anchor = anchorController.GetAnchor(type);
        item.transform.SetParent(anchor);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (howered)
            {
                DroppedIn?.Invoke();
            }

            if (InventoryPressed)
            {
                InventoryPressed = false;
                InventoryPressedEvent?.Invoke(InventoryPressed);
            }
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, float.MaxValue, Constants.Layers.RucksackdMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                InventoryPressed = true;
                InventoryPressedEvent?.Invoke(InventoryPressed);
            }

            if (!howered)
            {
                howered = true;
            }

            return;
        }

        if (howered)
        {
            howered = false;
        }
    }
}
