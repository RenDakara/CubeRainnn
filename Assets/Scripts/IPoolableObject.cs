using System;
using UnityEngine;

public interface IPoolableObject
{
    event Action<IPoolableObject> ReadyToReturn;
    void ResetState();
}
