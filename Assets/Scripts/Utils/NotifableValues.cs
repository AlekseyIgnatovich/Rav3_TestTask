using System;
using UnityEngine;


[Serializable]
public class NotifableValue<T>
{
    [SerializeField] T value = default;

    public event Action<T> Changed;

    public T Value
    {
        get { return value; }
        set
        {
            this.value = value;
            Changed?.Invoke(value);
        }
    }

}

[Serializable]
public class NotifableBool : NotifableValue<bool>{}

[Serializable]
public class NotifableInt : NotifableValue<int> { }

[Serializable]
public class NotifableString : NotifableValue<string> { }