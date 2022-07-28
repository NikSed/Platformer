using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerSpawner _playerSpawner;
    private LevelSwitcher _levelSwitcher;

    void Start()
    {
        _playerSpawner = GameObject.FindObjectOfType<PlayerSpawner>();
        _levelSwitcher = GameObject.FindObjectOfType<LevelSwitcher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
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
        _playerSpawner.Spawn(0.25f);
    }
}
