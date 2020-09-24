using System;
using UnityEngine;

public class RucksackItemObject : MonoBehaviour
{
    public event Action<int, bool> DragStarted = null;

    public int InstanceId { get; private set; }
    public string SettingsId { get; private set; }
    public bool IsEquipped { get; private set; }
    public bool Dragging  { get; private set; }

    public void Init(int instanceId, string settingsId)
    {
        InstanceId = instanceId;
        SettingsId = settingsId;
    }

    public void SetEquipped(bool equipped)
    {
        IsEquipped = equipped;
        Dragging = false;
        var rigidbody = GetComponent<Rigidbody>();

        if (equipped)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
    }

    void OnMouseDown()
    {
        if (!IsEquipped)
        {
            Dragging = true;
            DragStarted?.Invoke(InstanceId, Dragging);
        }
    }

    void OnMouseUp()
    {
        Dragging = false;
        DragStarted?.Invoke(InstanceId, Dragging);
    }

    void Update()
    {
        if (Dragging)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.MaxValue, Constants.Layers.GroundMask))
            {
                Vector3 pos = hit.point;
                pos.y = transform.position.y;
                transform.position = pos;
            }
        }
    }
}
