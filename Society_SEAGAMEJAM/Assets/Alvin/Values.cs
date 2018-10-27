using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Values : IComparable<Values>
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int value;

    public Values(string newName, int newValue)
    {
        name = newName;
        value = newValue;
    }

    public int CompareTo(Values other)
    {
        if (other == null)
        {
            return 1;
        }

        return value - other.value;
    }
}