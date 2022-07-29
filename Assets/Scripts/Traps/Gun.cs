using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpawnDelay;
    [SerializeField] private bool _isFlip;

    private RaycastHit2D _hit;
    private Vector2 _rayDirection;
    private bool _isShooting;

    private void Start()
    {
        _rayDirection = Vector2.right;

        if (_isFlip)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _hit = Physics2D.Raycast(transform.localPosition, _rayDirection);

        Debug.DrawLine(transform.localPosition, _hit.point);

        if (_hit.collider && _hit.collider.CompareTag("Player") && !_isShooting)
        {
            _isShooting = true;
            StartCoroutine(Shooting());
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        _rayDirection = -Vector2.right;
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            GameObject.Instantiate(_bulletPrefab, transform.localPosition, transform.localRotation, transform);

            yield return new WaitForSeconds(_bulletSpawnDelay);

            _isShooting = false;

            StopCoroutine(Shooting());
        }

    }
}
