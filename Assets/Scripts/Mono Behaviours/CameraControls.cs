using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public void Pan(InputAction.CallbackContext ctx)
    {
        transform.position -= (Vector3)ctx.ReadValue<Vector2>();
    }
}
