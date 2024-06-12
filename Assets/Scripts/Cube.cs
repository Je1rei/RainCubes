using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : Shape<Cube>
{
    private bool _isCollided = false;
    [SerializeField] private SpawnerSphere _spawnerSphere;

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

    protected override void Reset()
    {
        base.Reset();
        _isCollided = false;
    }

    protected override IEnumerator LifeRoutine()
    {
        _renderer.material.color = UnityEngine.Random.ColorHSV();
        yield return base.LifeRoutine();
    }

    private void HandleCollision()
    {
        if (_isCollided == false)
        {
            _currentCoroutine = StartCoroutine(LifeRoutine());
            _isCollided = true;
        }
    }
}
