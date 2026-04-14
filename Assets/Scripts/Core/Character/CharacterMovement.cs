using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    private float activeMoveSpeed;
    float angle = 0;


    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.linearVelocity = moveInput * activeMoveSpeed;



        if (moveInput != Vector2.zero)
        {
            angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
