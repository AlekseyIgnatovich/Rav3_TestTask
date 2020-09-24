using System;
using UnityEngine;

public class RucksackMenu : MonoBehaviour
{
    public event Action<int> ItemSelected;

    [SerializeField] RucksackMenuItem[] menuItems;

    public void SetItemIcon(int index, int itemId, Sprite sprite)
    {
        menuItems[index].SetItem(itemId, sprite);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        foreach (var item in menuItems)
        {
            if (item.Pressed)
            {
                ItemSelected?.Invoke(item.ItemId);
            }
        }

        gameObject.SetActive(false);
    }
}
