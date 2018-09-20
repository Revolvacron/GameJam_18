using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineFreeLook))]
public class EVECameraControls : MonoBehaviour
{
    private Cinemachine.CinemachineFreeLook _freeLookCam;

    public float rigScale = 10.0f;
    public float maxZoomFactor = 10.0f;
    public float zoomSensitivity = 0.1f;
    public float movementEasingFactor = 2.0f;

    private SmoothFloat _zoom = new SmoothFloat(1.0f, 1.0f, 0.1f);
    private Vector2 _cameraMovement = new Vector2();

    // Use this for initialization
    void Start()
    {
        _freeLookCam = GetComponent<Cinemachine.CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        _zoom.transitionSpeed = Mathf.Abs(_zoom.targetValue - _zoom.currentValue) * 10;
        _zoom.targetValue = Mathf.Clamp(_zoom.targetValue + Input.mouseScrollDelta.y * zoomSensitivity, 1.0f, maxZoomFactor);
        _zoom.Update(Time.deltaTime);

        var shipCollider = _freeLookCam.m_Follow.GetComponent<Collider>();
        var width = Mathf.Max(shipCollider.bounds.extents.x, shipCollider.bounds.extents.y, shipCollider.bounds.extents.z);

        _freeLookCam.m_Orbits = new Cinemachine.CinemachineFreeLook.Orbit[3]
        {
            new Cinemachine.CinemachineFreeLook.Orbit(width * Mathf.PI / 2 * rigScale / _zoom, width * Mathf.PI / 8 * rigScale / _zoom),
            new Cinemachine.CinemachineFreeLook.Orbit(0, width * Mathf.PI / 2 * rigScale / _zoom),
            new Cinemachine.CinemachineFreeLook.Orbit(-width * Mathf.PI / 2 * rigScale / _zoom, width * Mathf.PI / 8 * rigScale / _zoom),
        };

        var input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (Input.GetMouseButton(0) && input.magnitude > 0.1f)
        {
            _cameraMovement = input;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            _cameraMovement = Vector2.zero;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        _freeLookCam.m_XAxis.m_InputAxisValue = _cameraMovement.x;
        _freeLookCam.m_YAxis.m_InputAxisValue = _cameraMovement.y;
    }
}
