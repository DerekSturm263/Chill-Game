using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float _panSpeed;
    [SerializeField] private float _orbitSpeed;
    [SerializeField] private float _zoomSpeed;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    private void OnDestroy()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        var touches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (touches.Count == 0)
            return;

        if (touches.Count == 1)
        {
            Pan(touches[0].delta);
        }
        else
        {
            //Orbit();
            Zoom(Vector2.Distance(touches[0].delta, touches[1].delta));
        }
    }

    private void Pan(Vector2 amount)
    {
        Vector3 localMovement = Camera.main.transform.right * amount.x + Camera.main.transform.up * amount.y;
        transform.position += localMovement * (_panSpeed * Time.deltaTime);
    }

    private void Orbit(float amount)
    {
        Vector3 rotation = Camera.main.transform.rotation.eulerAngles;
        rotation.y += amount * _orbitSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rotation);
    }

    private void Zoom(float amount)
    {
        Vector3 localMovement = Camera.main.transform.forward * amount;
        transform.position += localMovement * (_zoomSpeed * Time.deltaTime);
    }
}
