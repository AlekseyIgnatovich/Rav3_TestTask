using System;
using System.Collections.Generic;
using UnityEngine;

public class RucksackItemObject : MonoBehaviour
{
    public event Action<bool> DragStarted = null;

    public int InstanceId { get; private set; }
    public string SettingsId { get; private set; }

    bool dragging = false;

    public void Init(int instanceId, string settingsId)
    {
        InstanceId = instanceId;
        SettingsId = settingsId;
    }

    void OnMouseDown()
    {
        dragging = true;
        DragStarted?.Invoke(dragging);
    }

    void OnMouseUp()
    {
        dragging = false;
        DragStarted?.Invoke(dragging);
    }

    void Update()
    {
        if (dragging)
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
