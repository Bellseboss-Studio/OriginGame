using UnityEngine;

public class InputSystemCustom : MonoBehaviour
{
    private OriginGame _controller;
    private Vector2 _positionInScreen;
    
    public Vector2 PositionInScreen => _positionInScreen;

    void Awake()
    {
        _controller = new OriginGame();
        _controller.Player.Touch.performed += x => CallSomething(x.ReadValue<Vector2>());
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
