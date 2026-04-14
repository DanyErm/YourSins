using UnityEngine;
using Zenject;

public class MineCirclingProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private BoxCollider2D projectileCollider;
    [SerializeField] private float circlingVelocity = 5f;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float duration = 5f;

    [Inject] private Arena _arena;

    private Vector2 _projectileSize;
    private Vector2[] circlingProjectilePositions = new Vector2[4];
    private Vector2 targetPos;
    private float angleToTarget;


    private float rotationPercentage = 0f;
    private float movementPercentage = 0f;

    //private Transform previousTransform;
    private float previousRotation;
    private Vector2 previousPosition;


    private void Start()
    {
        _projectileSize = projectileCollider.bounds.size;
        previousRotation = transform.rotation.z;
        previousPosition = transform.position;

        circlingProjectilePositions = CalculateCornerPositions();
        targetPos = circlingProjectilePositions[ClosestCornerPos()];
    }

    private void Update()
    {
        if (duration > 0f)
        {
            CycleMovement();
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CycleMovement()
    {
        ////angleToTarget = Vector2.SignedAngle(transform.up, targetPos - (Vector2)transform.position);
        //angleToTarget = Vector2.SignedAngle(transform.right, targetPos - (Vector2)transform.position);


        Vector2 direction = targetPos - (Vector2)transform.position;
        angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angleToTarget) >= 0.1f)
        {
            rotationPercentage += Time.deltaTime * circlingVelocity;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(previousRotation, angleToTarget, rotationPercentage));
        }
        else if (Vector2.Distance(transform.position, targetPos) >= 0.1f)
        {
            movementPercentage += Time.deltaTime * movementSpeed;
            transform.position = Vector2.Lerp(previousPosition, targetPos, movementPercentage);
        }
        else
        {
            rotationPercentage = 0f;
            movementPercentage = 0f;

            targetPos = circlingProjectilePositions[NextCorner(true)];

            previousRotation = transform.rotation.z;
            previousPosition = transform.position;
        }
    }


    private float RotateToTarget(Transform previousTransform, float rotationPercentage)
    {
        return Mathf.LerpAngle(previousTransform.eulerAngles.z, angleToTarget, rotationPercentage);
    }

    private Vector2 MoveToTarget(Transform previousTransform, float movementPercentage)
    {
        return Vector2.Lerp(previousTransform.position, targetPos, movementPercentage);
    }


    private Vector2[] CalculateCornerPositions()
    {
        float leftX = _arena.GetPos().x - _arena.GetSize().x / 2 + _projectileSize.x / 2;
        float rightX = _arena.GetPos().x + _arena.GetSize().x / 2 - _projectileSize.x / 2;
        float topY = _arena.GetPos().y + _arena.GetSize().y / 2 - _projectileSize.y / 2;
        float botY = _arena.GetPos().y - _arena.GetSize().y / 2 + _projectileSize.y / 2;

        Vector2[] cornerPos = new Vector2[4];
        cornerPos[0] = new Vector2(leftX, topY);
        cornerPos[1] = new Vector2(rightX, topY);
        cornerPos[2] = new Vector2(rightX, botY);
        cornerPos[3] = new Vector2(leftX, botY);

        return cornerPos;
    }

    private int ClosestCornerPos()
    {
        float currentDistance;
        float minDistance = float.PositiveInfinity;
        int minDistanceElNum = -1;

        for (int i = 0; i < circlingProjectilePositions.Length; i++)
        {
            currentDistance = Vector2.Distance(circlingProjectilePositions[i], transform.position);

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                minDistanceElNum = i;
            }
        }
        return minDistanceElNum;
    }

    private int NextCorner(bool isClockwise)
    {
        int result;

        if (isClockwise)
        {
            result = ClosestCornerPos() + 1;
        }
        else
        {
            result = ClosestCornerPos() - 1;
        }

        if (result >= circlingProjectilePositions.Length)
        {
            return 0;
        }
        else if (result < 0)
        {
            return circlingProjectilePositions.Length - 1;
        }
        else
        {
            return result;
        }
    }
}