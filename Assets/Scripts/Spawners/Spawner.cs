using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<T> _pool;

    public event Action<int> AmountChanged;
    public event Action<int> ActiveChanged;

    public event Action<T> Spawned;
    public event Action<T> Returned;

    public int AmountCreated { get; private set; }
    public int ActiveAmount => _pool.CountActive;
    
    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => TakeObject(obj),
        actionOnRelease: (obj) => ReturnObject(obj),
        actionOnDestroy: (obj) => Destroy(),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public virtual void GetObject()
    {
        if (_pool.CountActive < _poolMaxSize)
            _pool.Get();
    }

    public void ReleasePool(T obj)
    {
        _pool.Release(obj);
    }

    public void ReturnObject(T obj)
    {
        Returned?.Invoke(obj);
        obj.gameObject.SetActive(false);
        ActiveChanged?.Invoke(ActiveAmount);
    }

    protected float GetRepeatRate() => _repeatRate;

    protected void SetStartPosition(Transform transform) => _startPoint = transform;

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected void TakeObject(T obj)
    {
        obj.transform.position = _startPoint.transform.position;
        obj.SetSpawner(this);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);

        AmountCreated++;

        ActiveChanged?.Invoke(ActiveAmount);
        AmountChanged?.Invoke(AmountCreated);
        Spawned?.Invoke(obj);
    }
}
