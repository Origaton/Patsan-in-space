using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(PlayerInput))]
public class OutsideCameraFollowScript : MonoBehaviour
{
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private float cameraLookSpeed;

    [Header("Camera zoom")]
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float maxCameraZoom;

    [Header("Camera collision")]
    [SerializeField] private float minDistanceToPlayer;
    [SerializeField] private float minCameraAngle;
    [SerializeField] private float maxCameraAngle;
    private float rotationX;
    private float rotationY;

    [Header("Other")]
    [SerializeField] private Transform targetTransform;
    private PlayerInput playerInput;
    private InputAction lookActoin;
    private InputAction zoomAction;

    [SerializeField] private Vector3 offset;
    private Vector2 lookInput;
    private Vector2 zoomInput;
    private Vector3 targetPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookActoin = playerInput.actions["Look"];
        zoomAction = playerInput.actions["Zoom"];

        offset = transform.position - targetTransform.position;

        //Блокировка курсора при запуске игры. Просто для удобства
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateCamera();
        HandleCameraZoom();
    }

    private void UpdateCamera()
    {
        lookInput = lookActoin.ReadValue<Vector2>();

        rotationX -= lookInput.y * cameraLookSpeed * Time.deltaTime;
        rotationY += lookInput.x * cameraLookSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, minCameraAngle, maxCameraAngle);

        Vector3 rotatedOffset = Quaternion.Euler(rotationX, rotationY, 0f) * offset;
        Vector3 adjustedOffset = CollisionHandle(rotatedOffset);
        targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position + adjustedOffset, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

        transform.LookAt(targetTransform);
    }

    private void HandleCameraZoom()
    {
        zoomInput = zoomAction.ReadValue<Vector2>();
        offset.z += zoomInput.y * cameraZoomSpeed * Time.deltaTime;
        offset.z = Mathf.Clamp(offset.z, -maxCameraZoom, -minCameraZoom);
    }

    private Vector3 CollisionHandle(Vector3 rotatedOffset)
    {
        Vector3 direction = rotatedOffset.normalized;
        float distance = rotatedOffset.magnitude;

        if (Physics.Raycast(targetTransform.position, direction, out RaycastHit hit, distance))
        {
            distance = Mathf.Max(hit.distance - 0.1f, minDistanceToPlayer);
        }

        return direction * distance;
    }
}
