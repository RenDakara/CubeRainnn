using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject
{
    [SerializeField] protected Transform startPoint;
    [SerializeField] protected T prefab;
    [SerializeField] protected float repeatRate = 6f;
    [SerializeField] protected int poolCapacity = 5;
    [SerializeField] protected int poolMaxSize = 5;

    protected ObjectPool<T> pool;
    private Coroutine spawnCoroutine;

    public event System.Action OnPoolChanged;

    protected virtual void Awake()
    {
        pool = new ObjectPool<T>(
            () => Instantiate(prefab),
            OnGet,
            OnRelease,
            obj => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: poolCapacity,
            maxSize: poolMaxSize);
    }

    protected virtual void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(repeatRate);
        }
    }

    protected virtual void OnDestroy()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }

    protected virtual void OnGet(T obj)
    {
        obj.transform.position = startPoint.position;
        obj.gameObject.SetActive(true);
        obj.ResetState();

        OnPoolChanged?.Invoke();
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        OnPoolChanged?.Invoke();
    }

    protected virtual void Spawn()
    {
        pool.Get();
    }

    public int GetActiveCount() => pool.CountActive;
    public int GetCreatedCount() => pool.CountInactive + pool.CountActive;

}