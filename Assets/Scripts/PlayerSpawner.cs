using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private GameObject _player;
    private GameObject _spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        DestroyPlayer();

        _player = GameObject.Instantiate(_playerPrefab);
        _spawnPoint = GetSpawnPoint();

        _player.transform.localScale = _spawnPoint.transform.localScale;
        _player.transform.position = _spawnPoint.transform.position;
    }

    public void Spawn(float delay)
    {
        DestroyPlayer();
        Invoke("Spawn", delay);
    }

    private GameObject GetSpawnPoint()
    {
        return GameObject.FindGameObjectWithTag("SpawnPoint");
    }

    private void DestroyPlayer()
    {
        if (_player != null)
        {
            Destroy(_player);
        }
    }
}
