using UnityEngine;

public class RockHead : MonoBehaviour
{
    [SerializeField] private float _attackSpeed = 5f;
    [SerializeField] private float _killDistance = 1f;
    [SerializeField] private AttackDirection _attackDirection;
    [SerializeField] private Collider2D[] _collidersCheckPlayerHit;
    private enum AttackDirection { Down, Up, Left, Right }

    private float _returningSpeed;

    private Vector3 _startPos;
    private Vector3 _smashPoint;

    private bool _isReturning = false;
    private bool _isAttacking = false;


    void Start()
    {
        _returningSpeed = _attackSpeed / 500f;
        _startPos = transform.localPosition;
        _smashPoint = GetSmashPoint();
    }

    private void FixedUpdate()
    {
        if (_isReturning)
        {
            Return();
            return;
        }

        if (_isAttacking)
        {
            Attack();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(_startPos, GetChosenDirection());
        Debug.DrawLine(_startPos, hit.point, Color.red);

        if (hit.collider.CompareTag("Player"))
        {
            _isAttacking = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isReturning = true;
            _isAttacking = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.tag = "Platform";
            DisableTriggers();
        }
    }

    private void Attack()
    {
        transform.Translate(_attackSpeed * Time.fixedDeltaTime * GetChosenDirection());

        float distance = Vector3.Distance(transform.localPosition, _smashPoint);

        if (distance < _killDistance)
        {
            GetChosenCollider().enabled = true;
            gameObject.tag = "Enemy";
        }
    }

    private void Return()
    {
        if (transform.localPosition == _startPos)
        {
            _isReturning = false;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _startPos, _returningSpeed);
    }

    //Направление движения ловушки
    private Vector2 GetChosenDirection()
    {
        Vector2 dir;

        switch (_attackDirection)
        {
            case AttackDirection.Left:
                dir = Vector2.left;
                break;
            case AttackDirection.Right:
                dir = Vector2.right;
                break;
            case AttackDirection.Up:
                dir = Vector2.up;
                break;
            case AttackDirection.Down:
                dir = Vector2.down;
                break;
            default:
                dir = Vector2.down;
                break;
        }

        return dir;
    }

    //Нужный тригер для проверки столкновения с игроком
    private Collider2D GetChosenCollider()
    {
        Collider2D collider;
        switch (_attackDirection)
        {
            case AttackDirection.Down:
                collider = _collidersCheckPlayerHit[0];
                break;
            case AttackDirection.Up:
                collider = _collidersCheckPlayerHit[1];
                break;
            case AttackDirection.Left:
                collider = _collidersCheckPlayerHit[2];
                break;
            case AttackDirection.Right:
                collider = _collidersCheckPlayerHit[3];
                break;
            default:
                collider = _collidersCheckPlayerHit[0];
                break;
        }
        return collider;
    }

    private void DisableTriggers()
    {
        foreach (var item in _collidersCheckPlayerHit)
        {
            item.enabled = false;
        }
    }

    //Точка куда бьет ловушка
    private Vector3 GetSmashPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(_startPos, GetChosenDirection());

        if (hit.collider == null)
        {
            Debug.LogWarning("Rock Head trap no find smash point because no platform another side");
        }

        return hit.point;
    }
}
