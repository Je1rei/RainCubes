using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public abstract class Shape<T> : MonoBehaviour, ISpawnable, IExplodable where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Color _defaultColor;
    private float _lifeTime;

    private Spawner<T> _spawner;
    private Renderer _renderer;

    public event Action<Transform> Died;

    protected Material Material => _renderer.material;

    protected void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void SetSpawner<TSpawner>(Spawner<TSpawner> spawner) where TSpawner : MonoBehaviour, ISpawnable
    {
        _spawner = spawner as Spawner<T>;
    }

    protected float RandomizeLifeTime() => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

    protected void SetMaterial(Material material) => _renderer.material = material;

    protected void SetColor(Color color) => _renderer.material.color = color;

    protected void FadeMaterial() => _renderer.material.DOFade(0f, _lifeTime).SetEase(Ease.Linear);

    protected virtual void Initialize()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        _lifeTime = RandomizeLifeTime();
    }

    protected virtual IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);

        _spawner.ReleasePool((T)(object)this);
        Died?.Invoke(transform);
        Reset();
    }

    protected virtual void Reset()
    {
        _renderer.material.color = _defaultColor;
    }
}
