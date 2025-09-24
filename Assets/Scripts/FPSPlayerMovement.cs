using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController_NewInputSystem : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("Look")]
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isRunning;
    private bool isJumping;
    private bool isGrounded;
    private float verticalRotation = 0f;
    private Vector3 velocity;

    // Input System
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction runAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        // Получаем Actions
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        runAction = playerInput.actions["Run"];

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Чтение ввода
        moveInput = moveAction.ReadValue<Vector2>();
        isRunning = runAction.IsPressed();
        isJumping = jumpAction.triggered && isGrounded;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y) * currentSpeed;
        controller.Move(move * Time.deltaTime);

        // Прыжок
        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        lookInput = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;

        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * lookInput.x);
    }
}