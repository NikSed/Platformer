using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpForce = 16f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    [SerializeField] private int _maxJumpCount = 1;

    private float _currentJumpTime;
    private int _currentJumpCount;
    private bool _isJumping;
    private Animator _animator;
    private Rigidbody2D _rigidBody2D;
    private DynamicJoystick _joystick;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        float inputKeybordHorizontal = Input.GetAxisRaw("Horizontal");
        float inputJoystickHorizontal = _joystick.Horizontal;

        Move(inputKeybordHorizontal + inputJoystickHorizontal);

        if (_isJumping)
        {
            Jump();
        }
    }

    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            _currentJumpTime += Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _currentJumpCount++;
            _isJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _currentJumpTime = 0;
            _isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger || collision.CompareTag("Platform"))
        {
            _currentJumpCount = 0;
            FindObjectOfType<MobileJumpButton>().SetJumpingCount();
        }
    }

    private void Initialize()
    {
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _joystick = GameObject.FindObjectOfType<DynamicJoystick>();
    }

    private void FlipPlayer(float inputValue)
    {
        if (inputValue > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Jump()
    {
        if (_currentJumpTime < _maxJumpTime && _currentJumpCount <= _maxJumpCount && _rigidBody2D.velocity.y < _jumpForce * 5)
        {
            _rigidBody2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Jump(float jumpTime, int jumpCount)
    {
        if (jumpTime < _maxJumpTime && jumpCount <= _maxJumpCount)
        {
            _rigidBody2D.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Move(float inputValue)
    {
        if (inputValue != 0)
        {
            FlipPlayer(inputValue);

            _rigidBody2D.AddForce(new Vector2(inputValue, 0f) * _moveSpeed, ForceMode2D.Impulse);
        }

        _animator.SetFloat("InputX", Mathf.Abs(inputValue));

        _animator.SetFloat("Velocity", _rigidBody2D.velocity.y);
    }
}
