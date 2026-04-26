using System;
using UnityEngine;

public interface IInventoryManager
{
    int TotemsCollected { get; }
    event Action<int> OnTotemsCountChanged;
    void AddTotem();
}
