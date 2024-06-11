using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public abstract class Shape : MonoBehaviour, ISpawnable
{
    [SerializeField] protected float _minLifeTime = 2f;
    [SerializeField] protected float _maxLifeTime = 5f;

    private Color _defaultColor;

    protected Spawner<Shape> _spawner;
    protected Renderer _renderer;

    protected void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    public void SetSpawner<T>(Spawner<T> spawner) where T : MonoBehaviour, ISpawnable
    {
        _spawner = spawner as Spawner<Shape>;
    }

    protected float RandomizeLifeTime() => Random.Range(_minLifeTime, _maxLifeTime);

    protected virtual IEnumerator LifeRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(RandomizeLifeTime());

        yield return waitTime;

        _spawner.ReleasePool(this);
        Reset();
    }

    protected virtual void Reset()
    {
        _renderer.material.color = _defaultColor;
    }
}
