using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class StatValue
{
    public int currentValue;

    [NonSerialized]
    public Action<int> OnValueChanged = delegate { };

    public StatValue(int _currentValue)
    {
        currentValue = _currentValue;
    }

    public void AddToCurrent(int _value)
    {
        SetCurrentValue(currentValue + _value);
    }

    public void SetCurrentValue(int _value)
    {
        currentValue = _value;
        OnValueChanged(currentValue);
    }

    public override string ToString()
    {
        return currentValue.ToString();
    }
}
