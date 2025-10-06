using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(PlayerInput))]
public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private float cameraFollowSpeed = 0.05f;
    [SerializeField] private float cameraLookSpeed = 20f;
    private float rotationX;
    private float rotationY;

    [SerializeField] private Transform targetTransform;
    private PlayerInput playerInput;
    private InputAction lookActoin;

    [SerializeField] private Vector3 offset;
    private Vector2 lookInput;
    private Vector3 targetPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookActoin = playerInput.actions["look"];

        offset = transform.position - targetTransform.position;

        //Блокировка курсора при запуске игры. Просто для удобства
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        lookInput = lookActoin.ReadValue<Vector2>();

        rotationX -= lookInput.y * cameraLookSpeed * Time.deltaTime;
        rotationY += lookInput.x * cameraLookSpeed * Time.deltaTime;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Vector3 rotatedOffset = Quaternion.Euler(rotationX, rotationY, 0f) * offset;
        targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position + rotatedOffset, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

        //Доработать этот моментик
        transform.LookAt(targetTransform);
    }
}
