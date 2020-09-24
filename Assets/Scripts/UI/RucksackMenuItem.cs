using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RucksackMenuItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public bool Pressed { get; private set; }
    public int ItemId { get; private set; }

    void Awake()
    {
        ItemId = Constants.UnEquippedItemId;
    }

    public void SetItem(int itemId, Sprite sprite)
    {
        this.ItemId = itemId;

        var image = GetComponent<Image>();
        image.sprite = sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Pressed = true;
    }

    void OnDisable()
    {
        Pressed = false;
    }
}
