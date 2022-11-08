using Hexagons;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastPlayer : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform raycastPlace;
    private Vector2 _mousePosition;

    public void Click(InputAction.CallbackContext contex)
    {
        Debug.Log($" started {contex.started}");
        //Debug.Log($" canceled {contex.canceled}");
        //Debug.Log($" performed {contex.performed}");
        if (contex.canceled)
        {
            ShootRaycast();   
        }
    }
    public void Point(InputAction.CallbackContext contex)
    {
        _mousePosition = contex.ReadValue<Vector2>();
        Debug.Log($"{_mousePosition}");
    }

    private void ShootRaycast()
    {
        RaycastHit hit;
        if (_mousePosition.x <= Screen.width && _mousePosition.y <= (Screen.height * 0.86f))
        {
            Ray ray = mainCamera.ScreenPointToRay(_mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var objectHit = hit.transform;
                if (objectHit.gameObject.TryGetComponent<HexagonTemplateCollider>(out var template))
                {
                    template.Click();
                }
            }
        }
    }
}
