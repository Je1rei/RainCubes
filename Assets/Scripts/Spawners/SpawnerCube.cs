public class SpawnerCube : Spawner<Cube> 
{
    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, GetRepeatRate());
    }
}