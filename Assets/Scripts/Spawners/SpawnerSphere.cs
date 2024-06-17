using UnityEngine;

public class SpawnerSphere : Spawner<Sphere>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private Cube _currentCube;

    private void OnEnable()
    {
        _spawnerCube.Spawned += SetCube;
    }

    private void OnDisable()
    {
        _spawnerCube.Spawned -= SetCube;
    }

    private void SetCube(Cube cube)
    {
        _currentCube = cube;
        _currentCube.Died += Spawn;
    }

    private void Spawn(Transform transform)
    {
        SetStartPosition(transform);
        GetObject();
    }
}
