using UnityEngine;
using Zenject;

public class ArenaLeftSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Arena _arena;
    private Vector2 _spawnLocation;

    public override void Spawn(float x, float y, GameObject projectile)
    {
        _spawnLocation = CalculateSpawnLocation(x, y);

        _container.InstantiatePrefab(projectile, _spawnLocation, Quaternion.Euler(0, 0, 90), null);
    }

    private Vector3 CalculateSpawnLocation(float x, float y)
    {
        return (Vector2)_arena.GetPos() + new Vector2(-y - _arena.GetSize().x / 2, x);
    }
}