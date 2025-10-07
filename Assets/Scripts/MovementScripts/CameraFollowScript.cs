using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(PlayerInput))]
public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] private float cameraLookSpeed;
    [SerializeField] private float minCameraAngle;
    [SerializeField] private float maxCameraAngle;

    [Header("Camera zoom")]
    [SerializeField] private float cameraZoomSpeed;
    [SerializeField] private float minCameraZoom;
    [SerializeField] private float maxCameraZoom;
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

    private void LateUpdate()
    {
        UpdateCamera();
        HandleCameraZoom();
    }

    private void UpdateCamera()
    {
        lookInput = lookActoin.ReadValue<Vector2>();

        rotationX -= lookInput.y * cameraLookSpeed * Time.deltaTime;
        rotationY += lookInput.x * cameraLookSpeed * Time.deltaTime;

        //Лучше переделать на коллизию, наверное
        rotationX = Mathf.Clamp(rotationX, minCameraAngle, maxCameraAngle);

        Vector3 rotatedOffset = Quaternion.Euler(rotationX, rotationY, 0f) * offset;
        targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position + rotatedOffset, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

        transform.LookAt(targetTransform);
    }

    private void HandleCameraZoom()
    {
        zoomInput = zoomAction.ReadValue<Vector2>();
        offset.z += zoomInput.y * cameraZoomSpeed * Time.deltaTime;
        offset.z = Mathf.Clamp(offset.z, -maxCameraZoom, -minCameraZoom);
    }
}
