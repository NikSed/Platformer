using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class LaserGun : MonoBehaviour
{
    [SerializeField] private float _rotationDegree = 60f;
    [SerializeField] private float _rotationSpeed = 0.5f;
    [SerializeField] private bool _isRotate360;

    [SerializeField] private ParticleSystem _laserParticle;

    private LineRenderer _lineRenderer;
    private RaycastHit2D hit;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
    }

    private void FixedUpdate()
    {
        Work();

        Rotate();
    }

    private void Work()
    {
        hit = Physics2D.Raycast(transform.localPosition, transform.TransformDirection(Vector3.down));

        if (hit.collider)
        {
            if (_laserParticle.isStopped)
            {
                _laserParticle.Play();
            }

            _laserParticle.transform.localPosition = hit.point;

            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<Player>().Kill();
            }
        }
        else
        {
            if (_laserParticle.isPlaying)
            {
                _laserParticle.Stop();
            }
        }

        _lineRenderer.SetPosition(0, transform.localPosition);
        _lineRenderer.SetPosition(1, hit.point);
    }

    private void Rotate()
    {
        if (_isRotate360)
        {
            transform.Rotate(0, 0, 1f + _rotationSpeed);
        }
        else
        {
            float angle = -_rotationDegree * Mathf.Sin(Time.time * _rotationSpeed);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
