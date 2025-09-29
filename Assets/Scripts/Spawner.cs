using UnityEngine;
using UnityEngine.Pool;
using System;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _startPoint;

    [SerializeField] private float _repeatRate = 6f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            () => Instantiate(_prefab),
            obj => OnGet(obj),
            obj => obj.gameObject.SetActive(false),
            obj => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, _repeatRate);
    }

    private void OnGet(T obj)
    {
        obj.transform.position = _startPoint.position;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        obj.gameObject.SetActive(true);

        obj.OnReadyToReturn -= ReleaseObject;
        obj.OnReadyToReturn += ReleaseObject;

        obj.ResetState();
    }

    private void SpawnObject()
    {
        _pool.Get();
    }

    private void ReleaseObject(MonoBehaviour obj)
    {
        if (obj is T tObj)
            _pool.Release(tObj);
    }
}
