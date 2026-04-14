using UnityEngine;

public class SpinningProjectileBehaviour : MonoBehaviour
{
    private Vector2 _flightDirection = Vector2.down;

    [SerializeField] private float startSpeed = 0;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Animator spinningAnimator;


    private void OnEnable()
    {
        _flightDirection = transform.TransformDirection(Vector2.left);
    }

    private void Update()
    {
        transform.Translate(_flightDirection * startSpeed * Time.deltaTime, Space.World);
    }

    public void StartRealMovement()
    {
        startSpeed = movementSpeed;
        boxCollider2D.enabled = true;
        spinningAnimator.Play("SpinningAnimation");
    }
}