using UnityEngine;

public interface ISpawnable
{
    void SetSpawner<T>(Spawner<T> spawner) where T : MonoBehaviour, ISpawnable;
}
