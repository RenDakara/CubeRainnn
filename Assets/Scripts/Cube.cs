using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPoolableObject
{
    private Coroutine _coroutine;
    private bool isTouched = false;
    private HashSet<int> _touchedLayers = new HashSet<int>();
    private int[] _layers;
    private int _random;
    private WaitForSeconds _wait;
    public event Action<MonoBehaviour> OnReadyToReturn;

    private void Awake()
    {
        _random = UnityEngine.Random.Range(2, 6);
        _wait = new WaitForSeconds(_random);

        _layers = new int[]
        {
            LayerMask.NameToLayer("Plane1"),
            LayerMask.NameToLayer("Plane2"),
            LayerMask.NameToLayer("Plane3")
        };

    }

    private void OnCollisionEnter(Collision collision)
    {
        int colLayer = collision.gameObject.layer;

        if (Array.IndexOf(_layers, colLayer) >= 0 && !_touchedLayers.Contains(colLayer))
        {
            ChangeColor(gameObject);
            _touchedLayers.Add(colLayer);
            _coroutine = StartCoroutine(DestroyCube());
        }
    }

    private void ChangeColor(GameObject obj)
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        Renderer renderer = obj.GetComponent<Renderer>();
        renderer.material.color = color;
    }

    private IEnumerator DestroyCube()
    {
        yield return _wait;
        ResetState();
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = Color.white;
        OnReadyToReturn?.Invoke(this);
    }

    public void ResetState()
    {
        _touchedLayers.Clear();
    }
}