using UnityEngine;

public class StraightFlyingProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float preparationSpeed = 2f;
    [SerializeField] private float movementToPreparationSpeedRatio = 2.5f;
    [SerializeField] private BoxCollider2D boxCollider2D;


    private void Update()
    {
        transform.Translate(Vector2.up * preparationSpeed * Time.deltaTime, Space.Self);
    }

    public void StartRealMovement()
    {
        preparationSpeed *= -movementToPreparationSpeedRatio;
        boxCollider2D.enabled = true;
    }
}