using UnityEngine;
using UnityEngine.Pool;

public class DualSpawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject
{
    [SerializeField] public Transform startPoint;
    [SerializeField] private T prefab;
    [SerializeField] private float repeatRate = 6f;
    [SerializeField] private int poolCapacity = 5;
    [SerializeField] private int poolMaxSize = 5;

    protected ObjectPool<T> pool;

    private void Awake()
    {
        pool = new ObjectPool<T>(
            () => Instantiate(prefab),
            obj => OnGet(obj),
            obj => OnRelease(obj),
            obj => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: poolCapacity,
            maxSize: poolMaxSize);
    }

    protected virtual void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, repeatRate);
    }

    protected virtual void OnGet(T obj)
    {
        obj.transform.position = startPoint.position;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        obj.gameObject.SetActive(true);

        obj.ReadyToReturn -= ReturnToPool;
        obj.ReadyToReturn += ReturnToPool;

        obj.ResetState();
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected virtual void Spawn()
    {
        pool.Get();
    }

    protected virtual void ReturnToPool(IPoolableObject poolable)
    {
        if (poolable is T tObj)
            pool.Release(tObj);
    }

    public int GetActiveCount() => pool.CountActive;
    public int GetCreatedCount() => pool.CountInactive + pool.CountActive;
}