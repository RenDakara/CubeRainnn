using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolableObject
{
    void ResetState();
    event Action<IPoolableObject> ReadyToReturn;
}
