using System.Collections;
using UnityEngine;
using Zenject;

public class ChasingSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Character _char;


    public override void Spawn(float howOften, float howLong, GameObject projectile)
    {
        StartCoroutine(PlaceProjectiles(howOften, howLong, projectile));
    }


    private IEnumerator PlaceProjectiles(float howOften, float howLong, GameObject projectile)
    {
        while (howLong > howOften)
        {
            yield return new WaitForSeconds(howOften);

            _container.InstantiatePrefab(projectile, _char.GetPos(), _char.GetRotation(), null);

            howLong -= howOften;
        }
    }
}