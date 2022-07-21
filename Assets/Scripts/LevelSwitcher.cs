using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelsPrefabs;
    [SerializeField] private int _currentLevelID = 0;

    private void Start()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int levelID)
    {
        LevelDestroy();

        _currentLevelID = levelID;

        GameObject level = GameObject.Instantiate(_levelsPrefabs[levelID]);
        level.transform.parent = gameObject.transform;
        level.transform.localPosition = _levelsPrefabs[levelID].transform.position;
    }

    public void LoadNextLevel()
    {
        _currentLevelID++;

        if (_currentLevelID == _levelsPrefabs.Count)
        {
            _currentLevelID--;
        }

        LevelDestroy();

        GameObject level = GameObject.Instantiate(_levelsPrefabs[_currentLevelID]);
        level.transform.parent = gameObject.transform;
        level.transform.localPosition = _levelsPrefabs[_currentLevelID].transform.position;
    }

    private void LevelDestroy()
    {

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

    }
}
