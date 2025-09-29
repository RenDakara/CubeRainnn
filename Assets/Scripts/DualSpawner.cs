using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class DualSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _startPoint;

    [SerializeField] private float _repeatRate = 6f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    [SerializeField] private TextMeshProUGUI _text;

    private ObjectPool<Cube> _cubePool;
    private ObjectPool<Bomb> _bombPool;

    private int _totalCubeSpawned = 0;
    private int _totalCubeCreated = 0;

    private int _totalBombSpawned = 0;
    private int _totalBombCreated = 0;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            () => { _totalCubeCreated++; return Instantiate(_cubePrefab); },
            obj => PrepareCube(obj),
            obj => obj.gameObject.SetActive(false),
            obj => Destroy(obj.gameObject),
            false, _poolCapacity, _poolMaxSize);

        _bombPool = new ObjectPool<Bomb>(
            () => { _totalCubeCreated++; return Instantiate(_bombPrefab); },
            obj => PrepareBomb(obj),
            obj => obj.gameObject.SetActive(false),
            obj => { },
            false, _poolCapacity, _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0f, _repeatRate);
    }

    private void Update()
    {
        _text.text = ($"Cubes: Spawned={GetTotalCubeSpawned()}, Created={GetCreatedCubeCount()}, Active={GetActiveCubeCount()}, Bombs: Spawned= {GetTotalBombSpawned()}, Created={GetCreatedBombCount()}, Active={GetActiveBombCount()}");
    }

    private void PrepareCube(Cube cube)
    {
        cube.transform.position = _startPoint.position;
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.velocity = Vector3.zero;

        cube.gameObject.SetActive(true);
        cube.ResetState();

        cube.OnReadyToReturn -= OnCubeReturned;
        cube.OnReadyToReturn += OnCubeReturned;

        _totalBombSpawned++;
    }

    private void PrepareBomb(Bomb bomb)
    {
        bomb.transform.position = _startPoint.position;
        Rigidbody rb = bomb.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        bomb.gameObject.SetActive(true);
        bomb.ResetState();

        bomb.OnReadyToReturn -= OnBombReturned;
        bomb.OnReadyToReturn += OnBombReturned;

        _totalBombSpawned++;
    }

    private void SpawnCube()
    {
        _cubePool.Get();
    }

    private void OnCubeReturned(MonoBehaviour cubeMono)
    {
        Cube cube = cubeMono as Cube;
        if (cube == null) return;

        Vector3 pos = cube.transform.position;
        _cubePool.Release(cube);

        Bomb bomb = _bombPool.Get();
        bomb.transform.position = pos;

        bomb.StartFadeAndExplode(() =>
        {
            if (bomb != null)
            {
                _bombPool.Release(bomb);
            }
        });
    }

    private void OnBombReturned(MonoBehaviour bombMono)
    {
        Bomb bomb = bombMono as Bomb;
        if (bomb == null) return;

        _bombPool.Release(bomb);
    }

    public int GetActiveCubeCount() => _cubePool.CountActive;
    public int GetCreatedCubeCount() => _totalCubeCreated;
    public int GetTotalCubeSpawned() => _totalCubeSpawned;

    public int GetActiveBombCount() => _bombPool.CountActive;
    public int GetCreatedBombCount() => _totalBombCreated;
    public int GetTotalBombSpawned() => _totalBombSpawned;
}