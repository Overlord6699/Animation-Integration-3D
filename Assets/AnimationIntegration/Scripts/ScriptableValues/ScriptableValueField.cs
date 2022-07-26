using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableValueField<T>
{
    [SerializeField]
    private ScriptableValue<T> scriptableValue;

    public bool HasValue => scriptableValue != null;

    public T Value
    {
        get => scriptableValue.Value;
        set => scriptableValue.Value = value;
    }
}
