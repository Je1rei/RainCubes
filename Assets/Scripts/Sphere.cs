using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]
public class Sphere : Shape<Sphere>
{
    protected override void Reset()
    {
        base.Reset();
    }

    protected override IEnumerator LifeRoutine()
    {
        _renderer.material.color = Random.ColorHSV();
        yield return base.LifeRoutine();
    }
}
