using UnityEngine;
using Zenject;

public class ArenaTopSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Arena _arena;
    private Vector2 _spawnLocation;

    public override void Spawn(float x, float y, GameObject projectile)
    {
        _spawnLocation = CalculateSpawnLocation(x, y);

        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        _container.InstantiatePrefab(projectile, _spawnLocation, rotation, null);

        //GameObject newProjectile = Instantiate(projectile, _spawnLocation, Quaternion.Euler(0, 0, projectile.transform.eulerAngles.z));
        //ProjectContext.Instance.Container.InjectGameObject(newProjectile);
    }

    private Vector2 CalculateSpawnLocation(float x, float y)
    {
        return (Vector2)_arena.GetPos() + new Vector2(x, y + _arena.GetSize().y / 2);
    }
}