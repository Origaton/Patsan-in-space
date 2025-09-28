using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TPSPlayerMovementAlternative : MonoBehaviour
{
    [Header("Input action")]
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction aimAction;
    private InputAction attackAction;

    [Header("Parametrs")]
    //[SerializeField] private float walkSpeed = 20f;
    [SerializeField] private float runSpeed = 100f;
    [SerializeField] private float jogSpeed = 50f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private float jumpForce = 5f;
    private Vector3 gravity;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    [Header("Bools")]
    private bool isRunning;
    private bool isJumping;

    [Header("Vectors")]
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;

    private CharacterController controller;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        gravity = Vector3.down * gravityStrength;

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        runAction = playerInput.actions["run"];
        aimAction = playerInput.actions["aim"];
        attackAction = playerInput.actions["attack"];
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    //Нужно поработать с движением в сторону взгляда, посмотреть как в других играх
    private void HandleMouseLook()
    {
        lookInput = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
        verticalRotation -= lookInput.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -30f, 30f);
        horizontalRotation += lookInput.x;
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }

    /* Мне не нравится как применяется гравитация. 
    Возможно стоит приклеить персонажа к земле, а во время прыжка убирать привязку
    Но в таком случае не очень понятно как быть с подением с выступа, например
    Надо думать */
    void HandleMovement()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        isRunning = runAction.IsPressed();
        float currentSpeed = isRunning ? runSpeed : jogSpeed;
        Vector3 forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        Vector3 moveDirection = forward * moveInput.y + cameraTransform.right * moveInput.x;
        Vector3 move = moveDirection * currentSpeed;
        if (jumpAction.WasPressedThisFrame() && controller.isGrounded)
        {
            velocity.y = jumpForce;
        }
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity += gravity * Time.deltaTime;
        Vector3 finalMove = move + velocity;
        controller.Move(finalMove * Time.deltaTime);
    }
}
