using UnityEngine;

public class Arena : MonoBehaviour
{
    private SpriteRenderer _arenaSprite;

    private void Start()
    {
        _arenaSprite = GetComponent<SpriteRenderer>();
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public Vector3 GetSize()
    {
        return _arenaSprite.bounds.size;
    }
}