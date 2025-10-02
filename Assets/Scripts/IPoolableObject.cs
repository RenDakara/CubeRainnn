using System;
using UnityEngine;

public interface IPoolableObject
{
    event Action<MonoBehaviour> ReadyToReturn;
    void ResetState();
}
