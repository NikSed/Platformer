using UnityEngine;


[RequireComponent(typeof(Camera))]

public class CameraResizer : MonoBehaviour
{
    [SerializeField]private float _defaultSize = 5f;

    private Camera _camera;
    private float _screenWidth;
    private float _screenHeight;
    private float _aspectRatio;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();

        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _aspectRatio = _screenWidth / _screenHeight;

        SetCameraSize();

    }

    private void SetCameraSize()
    {
        float newSize = _defaultSize;

        if (_aspectRatio <= 1.5f) newSize = 6.7f;
        else if (_aspectRatio > 1.5f && _aspectRatio <= 1.7f) newSize = 5.5f;
        else if (_aspectRatio > 1.7f && _aspectRatio <= 1.9f) newSize = 5f;
        else if (_aspectRatio > 1.9f) newSize = 4.5f;

        _camera.orthographicSize = newSize;
    }
}
