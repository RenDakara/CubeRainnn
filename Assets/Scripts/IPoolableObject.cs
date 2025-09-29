using System;
using UnityEngine;

public interface IPoolableObject
{
    event Action<MonoBehaviour> OnReadyToReturn;
    void ResetState();
}
