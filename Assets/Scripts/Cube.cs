using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CubeReturnHandler(Cube cube);

public class Cube : MonoBehaviour, IPoolableObject
{
    [SerializeField] private int[] _layers;
    private ColorChanger _colorChanger;
    private Coroutine _coroutine;
    private HashSet<int> _touchedLayers = new HashSet<int>();
    private int _random;
    private WaitForSeconds _wait;

    public event CubeReturnHandler ReadyToReturn;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _random = UnityEngine.Random.Range(2, 6);
        _wait = new WaitForSeconds(_random);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int colLayer = collision.gameObject.layer;

        if (Array.IndexOf(_layers, colLayer) >= 0 && !_touchedLayers.Contains(colLayer))
        {
            _colorChanger.ChangeColor(gameObject);
            _touchedLayers.Add(colLayer);

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(DestroyCube());
        }
    }

    private IEnumerator DestroyCube()
    {
        yield return _wait;
        ResetState();
        var renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;
        ReadyToReturn?.Invoke(this);
    }

    public void ResetState()
    {
        _touchedLayers.Clear();
    }
}