using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float _zAngleRotation = 2f;
    [SerializeField] private float _moveSpeed = 0.01f;
    [SerializeField] private List<Vector3> _movePoints;
    [SerializeField] private bool _isRepeat;

    private Vector3 _targetMovePoint;
    private Vector3 _startPoint;

    private void Start()
    {
        _startPoint = transform.localPosition;

        ////Если включено зацикливание, то убираем не нужную последнюю точку движения совпадающую с начальной точкой
        if (_isRepeat)
        {
            _movePoints.Remove(_startPoint);
        }
        ////
        

        ////Добавляем начальную точку движения в массив всех точек движения
        _movePoints.Insert(0, _startPoint);
        ////


        ////Выбираем следущую точку для движения после стартовой
        _targetMovePoint = _movePoints[1];
        ////
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, _zAngleRotation);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetMovePoint, _moveSpeed);

        if (transform.localPosition == _targetMovePoint)
        {
            SetMovePoint();
        }
    }

    private void SetMovePoint()
    {
        int index = _movePoints.IndexOf(_targetMovePoint);

        if (index == _movePoints.Count - 1)
        {
            if (!_isRepeat)
            {
                _movePoints.Reverse();
                index = 0;
            }
            else
            {
                index = -1;
            }
        }

        index++;

        _targetMovePoint = _movePoints[index];
    }
}
