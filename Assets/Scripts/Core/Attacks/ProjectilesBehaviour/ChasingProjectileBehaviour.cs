using UnityEngine;
using Zenject;

public class ChasingProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float duration = 10f;

    [Inject] private Character _char;


    private void Update()
    {
        if (duration > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_char.GetPos().y - transform.position.y, _char.GetPos().x - transform.position.x) * Mathf.Rad2Deg);
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}