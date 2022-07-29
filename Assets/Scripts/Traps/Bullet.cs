using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 4f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Kill();
        }
    }
}
