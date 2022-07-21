using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    private PlayerSpawner _playerSpawner;
    private LevelSwitcher _levelSwitcher;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _playerSpawner = GameObject.FindObjectOfType<PlayerSpawner>();
        _levelSwitcher = GameObject.FindObjectOfType<LevelSwitcher>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        /*if(_contactPoint2D.normalImpulse > 1f)
        {
            Kill();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Kill();
        }
        else if (collision.gameObject.CompareTag("EndLevelPoint"))
        {
            _levelSwitcher.LoadNextLevel();
            _playerSpawner.Spawn(0.25f);
        }
    }

    private void Kill()
    {
        //_rigidbody2D.velocity = Vector2.zero;
        _playerSpawner.Spawn(0.25f);
    }
}
