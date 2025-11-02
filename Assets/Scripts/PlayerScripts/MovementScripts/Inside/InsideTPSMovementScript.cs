using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class InsideTPSMovementScript : MonoBehaviour
{
    [SerializeField] private float jogSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravityMultiplier;
    private float currentSpeed;
    private float verticalVelocity;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;

    private Vector2 moveInput;
    private Vector3 moveDirection;

    private Quaternion targetRotation;
    private Quaternion playerRotation;

    private bool isRunning;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        moveAction = playerInput.actions["Move"];
        runAction = playerInput.actions["Run"];
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleGravitation();
    }

    public void HandleMovement()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        isRunning = runAction.IsPressed();
        currentSpeed = isRunning ? runSpeed : jogSpeed;

        if (moveInput == Vector2.zero)
        {
            moveDirection = Vector3.zero;
        }
        else
        {
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        }
        Vector3 finalMove = currentSpeed * moveDirection + Vector3.down * verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);
    }

    public void HandleRotation()
    {
        if (moveInput != Vector2.zero)
        {
            targetRotation = Quaternion.LookRotation(moveDirection);
            playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }

    public void HandleGravitation()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity += -Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
        else
        {
            verticalVelocity = 0f;
        }
    }
}
