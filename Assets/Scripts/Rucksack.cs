using System;
using System.Collections.Generic;
using UnityEngine;

public class Rucksack : MonoBehaviour
{
    public event Action<bool> InventoryHovered = null;
    public event Action<bool> InventoryPressedEvent = null;
    public event Action DroppedIn = null;

    public bool InventoryPressed { get; private set; }

    [SerializeField] AnchorController anchorController = null;

    RucksackItemsManager rucksackItemsManager = null;
    bool mouseDown = false;
    bool howered = false;

    public void Init(RucksackItemsManager rucksackItemsManager)
    {
        this.rucksackItemsManager = rucksackItemsManager;
    }

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
                Debug.Log("DroppedIn");
                DroppedIn?.Invoke();
            }

            if (InventoryPressed)
            {
                InventoryPressed = false;
                InventoryPressedEvent?.Invoke(InventoryPressed);
                Debug.Log("InventoryPressedEvent " + InventoryPressed);
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
                Debug.Log("InventoryPressedEvent " + InventoryPressed);
            }

            if (!howered)
            {
                howered = true;
                Debug.Log("InventoryHovered " + howered);
                InventoryHovered?.Invoke(howered);
            }

            return;
        }

        if (howered)
        {
            howered = false;
            Debug.Log("InventoryHovered " + howered);
            InventoryHovered?.Invoke(howered);
        }
    }
}
