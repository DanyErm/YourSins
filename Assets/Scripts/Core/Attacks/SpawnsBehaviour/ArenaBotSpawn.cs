using UnityEngine;
using Zenject;

public class ArenaBotSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Arena _arena;
    private Vector2 _spawnLocation;

    public override void Spawn(float x, float y, GameObject projectile)
    {
        _spawnLocation = CalculateSpawnLocation(x, y);

        Instantiate(projectile, _spawnLocation, Quaternion.Euler(0, 0, 180));
        _container.InstantiatePrefab(projectile, _spawnLocation, Quaternion.Euler(0, 0, 180), null);
    }

    private Vector3 CalculateSpawnLocation(float x, float y)
    {
        return (Vector2)_arena.GetPos() + new Vector2(-x, -y - _arena.GetSize().y / 2);
    }
}