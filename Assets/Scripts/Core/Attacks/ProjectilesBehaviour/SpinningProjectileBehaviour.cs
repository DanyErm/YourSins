using UnityEngine;

public class SpinningProjectileBehaviour : MonoBehaviour
{
    private Vector2 _flightDirection = Vector2.up;

    [SerializeField] private float startSpeed = 0;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private Animator spinningAnimator;
    [SerializeField] private BoxCollider2D boxCollider2D;


    private void OnEnable()
    {
        _flightDirection = transform.TransformDirection(Vector2.up);
    }

    private void Update()
    {
        transform.Translate(_flightDirection * startSpeed * Time.deltaTime, Space.World);
    }

    public void StartRealMovement()
    {
        startSpeed = -movementSpeed;
        boxCollider2D.enabled = true;
        spinningAnimator.Play("SpinningAnimation");
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}