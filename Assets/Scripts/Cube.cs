using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPoolableObject
{
    private ColorChanger _colorChanger;
    private Coroutine _coroutine;
    private HashSet<int> _touchedLayers = new HashSet<int>();
    private int[] _layers;
    private int _random;
    private string _layerName1 = "Plane1";
    private string _layerName2 = "Plane2";
    private string _layerName3 = "Plane3";
    private WaitForSeconds _wait;
    public event Action<MonoBehaviour> ReadyToReturn;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _random = UnityEngine.Random.Range(2, 6);
        _wait = new WaitForSeconds(_random);

        _layers = new int[]
        {
            LayerMask.NameToLayer(_layerName1),
            LayerMask.NameToLayer(_layerName2),
            LayerMask.NameToLayer(_layerName3)
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        int colLayer = collision.gameObject.layer;

        if (Array.IndexOf(_layers, colLayer) >= 0 && !_touchedLayers.Contains(colLayer))
        {
            _colorChanger.ChangeColor(gameObject);
            _touchedLayers.Add(colLayer);
            _coroutine = StartCoroutine(DestroyCube());
        }
    }

    private IEnumerator DestroyCube()
    {
        yield return _wait;
        ResetState();
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = Color.white;
        ReadyToReturn?.Invoke(this);
    }

    public void ResetState()
    {
        _touchedLayers.Clear();
    }
}