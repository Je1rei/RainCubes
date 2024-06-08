using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isCollided = false;
    private Color _defaultColor;

    private Spawner _spawner;
    private Renderer _renderer;

    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (_spawner != null)
            {
                HandleCollision();
            }
        }
    }

    public void SetSpawner(Spawner spawner) => _spawner = spawner;

    private float RandomizeLifeTime() => Random.Range(_minLifeTime, _maxLifeTime);

    private void HandleCollision()
    {
        if (_isCollided == false)
        {
            _currentCoroutine = StartCoroutine(LifeRoutine());
            _isCollided = true;
        }
    }

    private IEnumerator LifeRoutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(RandomizeLifeTime());
        _renderer.material.color = Random.ColorHSV();

        yield return waitTime;

        _spawner.ReleasePool(this);
        Reset();
    }

    private void Reset()
    {
        _isCollided = false;
        _renderer.material.color = _defaultColor;
    }
}
