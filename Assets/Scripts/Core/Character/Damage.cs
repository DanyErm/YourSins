using UnityEngine;
using Zenject;

public class Damage : MonoBehaviour
{
    [Inject] private Character _char;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            _char.HP--;
            Destroy(collision.gameObject);
        }
    }
}
