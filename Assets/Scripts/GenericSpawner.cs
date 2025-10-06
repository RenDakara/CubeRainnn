using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject
{
    [SerializeField] public Transform startPoint;
    [SerializeField] private T prefab;
    [SerializeField] private float repeatRate = 6f;
    [SerializeField] private int poolCapacity = 5;
    [SerializeField] private int poolMaxSize = 5;

    private Coroutine _spawnCoroutine;
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
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    protected virtual void OnDestroy()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(repeatRate);
        }
    }

    protected virtual void OnGet(T obj)
    {
        obj.transform.position = startPoint.position;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        obj.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    protected virtual void Spawn()
    {
        pool.Get();
    }

    public int GetActiveCount() => pool.CountActive;
    public int GetCreatedCount() => pool.CountInactive + pool.CountActive;
}