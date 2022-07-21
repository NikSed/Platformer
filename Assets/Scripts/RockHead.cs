using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RockHead : MonoBehaviour
{

    [SerializeField] private float _returningSpeed = 0.01f;
    [SerializeField] private float _attackSpeed = 10f;
    private Vector3 _startPos;
    private bool _isReturning = false;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _startPos = transform.localPosition;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isReturning)
        {
            Return();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, Vector2.down);

            if (hit.collider.CompareTag("Player"))
            {
                Attack();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isReturning = true;
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void Attack()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.AddForce(Vector3.down * _attackSpeed, ForceMode2D.Impulse);
    }

    private void Return()
    {
        if (transform.localPosition == _startPos)
        {
            _isReturning = false;
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _startPos, _returningSpeed);
    }
}
