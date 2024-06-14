using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radiusOverlap = 5f;
    [SerializeField] private float _power = 500f;
    [SerializeField] private SpawnerSphere _spawherSphere;

    private void OnEnable()
    {
        _spawherSphere.Returned += Explode;
    }

    private void OnDisable()
    {
        _spawherSphere.Returned -= Explode;
    }

    private void Explode(Sphere sphere)
    {
        Collider[] colliders = Physics.OverlapSphere(sphere.transform.position, _radiusOverlap);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IExplodable explodable))
            {
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

                rigidbody.AddExplosionForce(_power, collider.transform.position, _radiusOverlap);
            }
        }

    }
}
