using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Plant
{
    public string Name;
    public int TurnsToGrow;
    public int sellPrice;

    public UnityEvent OnUsed;
}