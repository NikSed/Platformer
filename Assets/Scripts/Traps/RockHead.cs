using UnityEngine;

public class RockHead : MonoBehaviour
{
    private enum AttackSide { Down, Up, Left, Right };

    [SerializeField] private BoxCollider2D[] _triggers;
    [SerializeField] private AttackSide _attackSide;
    [SerializeField] private float _attackSpeed = 4f;

    private Vector3 _currentAttackDirection;
    private Vector3 _startPosition;
    private Vector3 _smashPoint;
    private Vector3 _returnSmashPoint;

    private BoxCollider2D _currentTrigger;
    private BoxCollider2D _reverseCurrentTrigger;

    private bool _isAttacking = false;
    private bool _isReturning = false;

    private void Start()
    {
        _currentTrigger = CurrentTrigger(false);
        _reverseCurrentTrigger = CurrentTrigger(true);

        _currentAttackDirection = CurrentAttackDirection();

        _startPosition = transform.localPosition;

        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, _currentAttackDirection);
        _smashPoint = hit.point;

        hit = Physics2D.Raycast(transform.localPosition, -_currentAttackDirection);
        _returnSmashPoint = hit.point;

        //Debug.Log($"Start: {_startPosition}, Smash: {_smashPoint}, Return smash: {_returnSmashPoint}");
    }

    private void FixedUpdate()
    {
        if (_isAttacking)
        {
            Attack();
            return;
        }

        if (_isReturning)
        {
            Return();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(_startPosition, _currentAttackDirection);
        Debug.DrawLine(_startPosition, hit.point, Color.red);

        if(hit.collider == null)
        {
            Debug.LogWarning("No have smash point. Change attack side or move trap to another point");
            return;
        }

        if (hit.collider.CompareTag("Player"))
        {
            _isAttacking = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.tag = "Platform";

            _currentTrigger.enabled = false;

            _isAttacking = false;
            _isReturning = true;
        }
    }

    private void Attack()
    {
        transform.Translate(_currentAttackDirection * Time.deltaTime * _attackSpeed);

        if (Vector3.Distance(transform.localPosition, _smashPoint) < 0.8f)
        {
            _currentTrigger.enabled = true;

            gameObject.tag = "Enemy";
        }
    }

    private void Return()
    {
        if (transform.localPosition != _startPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _startPosition, 0.01f);

            if (Vector3.Distance(transform.localPosition, _returnSmashPoint) < 0.8f)
            {
                _reverseCurrentTrigger.enabled = true;

                gameObject.tag = "Enemy";
            }
        }
        else
        {
            gameObject.tag = "Platform";
            _reverseCurrentTrigger.enabled = false;
            _isReturning = false;
        }
    }

    private BoxCollider2D CurrentTrigger(bool reverse)
    {
        BoxCollider2D collider;

        if (!reverse)
        {
            switch (_attackSide)
            {
                case AttackSide.Up:
                    collider = _triggers[1];
                    break;
                case AttackSide.Left:
                    collider = _triggers[2];
                    break;
                case AttackSide.Right:
                    collider = _triggers[3];
                    break;
                default:
                    collider = _triggers[0];
                    break;
            }
        }
        else
        {
            switch (_attackSide)
            {
                case AttackSide.Up:
                    collider = _triggers[0];
                    break;
                case AttackSide.Left:
                    collider = _triggers[3];
                    break;
                case AttackSide.Right:
                    collider = _triggers[2];
                    break;
                default:
                    collider = _triggers[1];
                    break;
            }
        }

        return collider;
    }

    private Vector3 CurrentAttackDirection()
    {
        Vector3 direction;

        switch (_attackSide)
        {
            case AttackSide.Up:
                direction = Vector3.up;
                break;
            case AttackSide.Left:
                direction = Vector3.left;
                break;
            case AttackSide.Right:
                direction = Vector3.right;
                break;
            default:
                direction = Vector3.down;
                break;
        }

        return direction;
    }
}
