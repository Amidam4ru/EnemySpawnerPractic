using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _lifeTime = 5f;

    private Coroutine _releaseCorutine;
    private WaitForSeconds _releaseDelay;
    public event Action<Enemy> Died;

    private void Awake()
    {
        _releaseDelay = new WaitForSeconds(_lifeTime);
    }

    private void OnEnable()
    {
        _releaseCorutine = StartCoroutine(Release());
    }

    private void OnDisable()
    {
        StopCoroutine(_releaseCorutine);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator Release()
    {
        yield return _releaseDelay;
        Died?.Invoke(this);
    }
}
