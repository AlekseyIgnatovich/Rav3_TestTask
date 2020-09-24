using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RucksackMenuItem : MonoBehaviour
{
    int itemId = Constants.UnEquippedItemId;

    public void SetItem(int itemId, Sprite sprite)
    {
        this.itemId = itemId;

        var image = GetComponent<Image>();
        image.sprite = sprite;
    }

    public void Reset()
    {
        itemId = Constants.UnEquippedItemId;

        var image = GetComponent<Image>();
        image.sprite = null;
    }

    void OnMouseUp()
    {
        Debug.LogError("RucksackMenuItem OnMouseUp");

        //dragging = false;
        //DragStarted?.Invoke(InstanceId, dragging);
    }
}
