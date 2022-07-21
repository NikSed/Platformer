using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    [SerializeField] private float _rotationDegree = 45f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private bool _isCircle;

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!_isCircle)
        {
            float angle = _rotationDegree * Mathf.Sin(Time.time * _rotationSpeed);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            return;
        }

        transform.Rotate(0, 0, 1 * _rotationSpeed);
        
    }
}
