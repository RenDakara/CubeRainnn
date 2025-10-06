using System;
using System.Collections;
using UnityEngine;

public delegate void BombReturnHandler(Bomb bomb);

public class Bomb : MonoBehaviour, IPoolableObject
{
    private int _random;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    public event BombReturnHandler ReadyToReturn;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _exploder = GetComponent<Exploder>();
        _random = UnityEngine.Random.Range(2, 6);
        _wait = new WaitForSeconds(_random);
    }

    public void StartFadeAndExplode(Action onComplete)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

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
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
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
