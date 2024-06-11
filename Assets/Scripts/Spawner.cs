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

    public int AmountCreated { get; private set; }

    protected void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => TakeObject(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    protected void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);
    }

    public void ReleasePool(T obj)
    {
        _pool.Release(obj);
    }

    private void TakeObject(T obj)
    {
        obj.transform.position = _startPoint.transform.position;
        obj.SetSpawner(this);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.gameObject.SetActive(true);
        AmountCreated++;
    }

    private void GetObject()
    {
        if (_pool.CountActive < _poolMaxSize)
            _pool.Get();
    }
}
