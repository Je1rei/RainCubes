using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : Shape<Cube>
{
    private bool _isCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            if (GetSpawner() != null)
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
        base.Initialize();

        yield return base.LifeRoutine();
    }

    private void HandleCollision()
    {
        if (_isCollided == false)
        {
            ActivateCoroutine();
            _isCollided = true;
        }
    }
}
