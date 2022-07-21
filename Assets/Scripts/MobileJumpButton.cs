using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPressing;
    private float _pressingTime;
    private int _jumpCount = 0;

    private PlayerController _playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _jumpCount++;
        _isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressing = false;
        _pressingTime = 0;
    }

    public void SetJumpingCount(int count)
    {
        _jumpCount = count;
    }

    public void SetJumpingCount()
    {
        _jumpCount = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isPressing)
        {
            _pressingTime += Time.deltaTime;

            _playerController.Jump(_pressingTime, _jumpCount);
        }
    }
}
