using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolableObject
{
    private int _random;
    private Exploder _exploder;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    public event Action<MonoBehaviour> ReadyToReturn;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _random = UnityEngine.Random.Range(2, 6);
        _wait = new WaitForSeconds(_random);
    }

    public void StartFadeAndExplode(Action onComplete)
    {
        _coroutine = StartCoroutine(FadeAndExplodeCoroutine(() =>
        {
            ReadyToReturn?.Invoke(this);
            onComplete?.Invoke();
        }));
    }

    private IEnumerator FadeAndExplodeCoroutine(Action onComplete)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material material = renderer.material;
        Color color = material.color;

        float duration = _random;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f,0f,elapsed/duration);
            material.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        material.color = new Color(color.r, color.g, color.b, 0f);
        yield return _wait;

        _exploder.Explode();

        onComplete?.Invoke();
    }

    public void ResetState() { }   
}
