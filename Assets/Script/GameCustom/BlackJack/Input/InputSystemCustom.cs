using UnityEngine;

public class InputSystemCustom : MonoBehaviour
{
    private OriginGame _controller;
    private Vector2 _positionInScreen;
    
    public Vector2 PositionInScreen => _positionInScreen;

    void Awake()
    {
        _controller = new OriginGame();
        //_controller.Player.Touch.performed += x => CallSomething(x.ReadValue<Vector2>());
        //_controller.Player.StartTouch.performed += x => StartTouch(x.ReadValue<bool>());
        //_controller.Player.StartTouch.canceled += x => CancelTouch(x.ReadValue<bool>());
    }

    private void CancelTouch(bool cancel)
    {
        Debug.Log("Cancel Click");
    }

    private void StartTouch(bool perfomance)
    {
        Debug.Log("Start Click");
    }

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void CallSomething(Vector2 value)
    {
        Debug.Log($"vector {value}");
        _positionInScreen = value;
    }
}
