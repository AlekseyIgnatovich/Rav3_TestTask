using System;
using System.Collections.Generic;
using UnityEngine;

public class Rucksack : MonoBehaviour
{
    public event Action<bool> InventoryHovered = null;
    public event Action DroppedIn = null;

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
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;

            if(howered)
            {
                Debug.LogError("DroppedIn");
                DroppedIn?.Invoke();
            }
        }

        if (mouseDown)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.MaxValue, Constants.Layers.RucksackdMask))
            {
                if(!howered)
                {
                    howered = true;
                    Debug.LogError("InventoryHovered " + howered);
                    InventoryHovered?.Invoke(howered);
                }

                return;
            }

            if (howered)
            {
                howered = false;
                Debug.LogError("InventoryHovered " + howered);
                InventoryHovered?.Invoke(howered);
            }
        }
    }
}
