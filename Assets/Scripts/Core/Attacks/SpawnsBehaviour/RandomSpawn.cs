using System.Collections;
using UnityEngine;
using Zenject;

public class RandomSpawn : BaseSpawner
{
    [Inject] private DiContainer _container;
    [Inject] private Arena _arena;

    public override void Spawn(float howOften, float howLong, GameObject projectile)
    {
        StartCoroutine(PlaceProjectiles(howLong, howOften, projectile));
    }


    private Vector2 ChooseRandomPointInsideArena(Vector2 projectileSize)
    {
        float xMin = _arena.GetPos().x - _arena.GetSize().x / 2 + projectileSize.x / 2;
        float xMax = _arena.GetPos().x + _arena.GetSize().x / 2 - projectileSize.x / 2;
        float yMin = _arena.GetPos().y - _arena.GetSize().y / 2 + projectileSize.y / 2;
        float yMax = _arena.GetPos().y + _arena.GetSize().y / 2 - projectileSize.y / 2;

        return new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
    }

    private IEnumerator PlaceProjectiles(float howLong, float howOften, GameObject projectile)
    {
        Vector2 projectileSize = projectile.GetComponent<BoxCollider2D>().size;

        while (howLong > howOften)
        {
            yield return new WaitForSeconds(howOften);
            _container.InstantiatePrefab(projectile, ChooseRandomPointInsideArena(projectileSize), Quaternion.Euler(0, 0, 0), null);
            howLong -= howOften;
        }
    }
}

//-1. *Надо рандомизировать промежуток времени, который проходит между появлением снарядов (howOften)
// 2. *Надо добавить значение по умолчанию параметру projectileSize в методе ChooseRandomPointInsideArena, чтобы его можно было использовать без параметра