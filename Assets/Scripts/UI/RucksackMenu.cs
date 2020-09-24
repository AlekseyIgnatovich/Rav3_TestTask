using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RucksackMenu : MonoBehaviour
{
    [SerializeField] RucksackMenuItem[] menuItems;

    public void SetItemIcon(int index, int itemId, Sprite sprite)
    {
        menuItems[index].SetItem(itemId, sprite);
    }
}
