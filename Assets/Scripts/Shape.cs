using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public abstract class Shape<T> : MonoBehaviour, ISpawnable where T : MonoBehaviour, ISpawnable
{
    [SerializeField] protected float _minLifeTime = 2f;
    [SerializeField] protected float _maxLifeTime = 5f;

    private Color _defaultColor;

    protected Spawner<T> _spawner;
    protected Renderer _renderer;

    protected Coroutine _currentCoroutine;

    protected void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void SetSpawner<TSpawner>(Spawner<TSpawner> spawner) where TSpawner : MonoBehaviour, ISpawnable
    {
        _spawner = spawner as Spawner<T>;
    }

    protected float RandomizeLifeTime() => Random.Range(_minLifeTime, _maxLifeTime);

    protected virtual IEnumerator LifeRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(RandomizeLifeTime());

        yield return waitTime;

        _spawner.ReleasePool((T)(object)this);
        Reset();
    }

    protected virtual void Reset()
    {
        _renderer.material.color = _defaultColor;
    }
}
