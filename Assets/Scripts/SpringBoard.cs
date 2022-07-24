using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpringBoard : MonoBehaviour
{
    [SerializeField] private float _springPower = 10f;
    [SerializeField] private Collider2D _triger;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("isJump");

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * _springPower, ForceMode2D.Impulse);
        }
    }
}
