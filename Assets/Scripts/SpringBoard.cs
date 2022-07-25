using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpringBoard : MonoBehaviour
{
    [SerializeField] private float _springPower = 2f;

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
            rb.velocity = Vector2.down * 10f;
            rb.AddForce(Vector2.up * _springPower, ForceMode2D.Impulse);
        }
    }
}
