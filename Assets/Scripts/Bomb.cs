using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPoolableObject
{
    private int _random;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;
    private Renderer _renderer;
    public event Action<IPoolableObject> ReadyToReturn;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _renderer = gameObject.GetComponent<Renderer>();
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
        float duration = _random;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f,0f,elapsed/duration);
            _colorChanger.Fade(gameObject, alpha);
            yield return null;
        }

        _colorChanger.Fade(gameObject, 0f);
        yield return _wait;

        _exploder.Explode();

        onComplete?.Invoke();
    }

    public void ResetState() { }   
}
