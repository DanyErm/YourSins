using UnityEngine;
using Zenject;

public class CirclingProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private BoxCollider2D projectileCollider;
    [SerializeField] private float circlingVelocity = 5f;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float duration = 5f;

    [Inject] private Arena _arena;

    private Vector2 _projectileSize;
    private Vector2[] _circlingProjectilePositions = new Vector2[4];
    private Vector2 _targetPos;
    private float _targetAngle;

    private enum State { Rotating, Moving }
    private State _state;
    private float _t; // прогресс (0..1)
    private float _startRotation;
    private Vector2 _startPosition;

    private void Start()
    {
        _projectileSize = projectileCollider.bounds.size;
        _circlingProjectilePositions = CalculateCornerPositions();
        _targetPos = _circlingProjectilePositions[ClosestCornerPos()];

        // Начинаем с поворота к ближайшему углу
        _state = State.Rotating;
        _startRotation = transform.eulerAngles.z;
        _t = 0;
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
        if (_state == State.Rotating)
        {
            // Вычисляем целевой угол относительно transform.right
            Vector2 direction = _targetPos - (Vector2)transform.position;
            //_targetAngle = Vector2.SignedAngle(transform.right, direction);
            _targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _t += Time.deltaTime * circlingVelocity;
            if (_t >= 1f)
            {
                // Поворот завершён
                transform.rotation = Quaternion.Euler(0, 0, _targetAngle);
                _state = State.Moving;
                _startPosition = transform.position;
                _t = 0;
            }
            else
            {
                float newAngle = Mathf.LerpAngle(_startRotation, _targetAngle, _t);
                transform.rotation = Quaternion.Euler(0, 0, newAngle);
            }
        }
        else if (_state == State.Moving)
        {
            _t += Time.deltaTime * movementSpeed;
            if (_t >= 1f)
            {
                // Движение завершено
                transform.position = _targetPos;
                // Выбираем следующий угол (по часовой стрелке)
                _targetPos = _circlingProjectilePositions[NextCorner(true)];
                _state = State.Rotating;
                _startRotation = transform.eulerAngles.z;
                _t = 0;
            }
            else
            {
                transform.position = Vector2.Lerp(_startPosition, _targetPos, _t);
            }
        }
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

        for (int i = 0; i < _circlingProjectilePositions.Length; i++)
        {
            currentDistance = Vector2.Distance(_circlingProjectilePositions[i], transform.position);

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

        if (result >= _circlingProjectilePositions.Length)
        {
            return 0;
        }
        else if (result < 0)
        {
            return _circlingProjectilePositions.Length - 1;
        }
        else
        {
            return result;
        }
    }
}