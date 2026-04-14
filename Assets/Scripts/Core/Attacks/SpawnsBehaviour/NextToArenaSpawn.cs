using UnityEngine;
using Zenject;

public class NextToArenaSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Arena _arena;
    private Vector2 spawnLocation;


    public override void Spawn(float distance, float angle, GameObject projectile)
    {
        spawnLocation = _arena.GetPos() + CalculateAngleInfluence(distance, angle);

        Quaternion rotation = Quaternion.Euler(0, 0, -angle);
        _container.InstantiatePrefab(projectile, spawnLocation, rotation, null);
    }

    private Vector3 CalculateAngleInfluence(float distance, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float offsetX = distance * Mathf.Sin(rad);
        float offsetY = distance * Mathf.Cos(rad);

        return new Vector2(offsetX, offsetY);
    }
}