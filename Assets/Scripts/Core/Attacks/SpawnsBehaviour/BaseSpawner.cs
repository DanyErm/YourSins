using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    public abstract void Spawn(float param1, float param2, GameObject projectile);
}