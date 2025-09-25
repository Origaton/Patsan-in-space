using UnityEngine;
using UnityEngine.InputSystem;

public class TPSPlayerMovement : MonoBehaviour
{
    // Input System 
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction runAction;
    private InputAction aimAction;
    private InputAction attackAction;

        [Header("Move")]
    [SerializeField] private float walkSpeed = 20f; //скорость ходьбы
    [SerializeField] private float runSpeed = 100f; //скорость бега
    [SerializeField] private float jogSpeed = 50f; //скорость бега трусцой
    

        [Header("Look")]  // переменные для камеры
    [SerializeField] private float mouseSensitivity = 100f; // чувствительность мыши
    [SerializeField] private Transform cameraTransform; // берем позицию обекта "камера" , чтобы ее изменять
    
    
    private bool isRunning;
    private bool isJumping;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalRotation = 0f;
    private Vector3 velocity;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"]; // берем все возможные нажатия клавиш игроком
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        runAction = playerInput.actions["run"];
        aimAction = playerInput.actions["aim"];
        attackAction = playerInput.actions["attack"];
    }

    private void Update()
    {
        HandleMouseLook();// вызываем функцию вращения камеры
        HandleMovement(); //вызываем функцию перемещения
    }

    private void HandleMouseLook()
    {
        lookInput = lookAction.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime; // присваеваем переменной движения мыши и умножаем на чуствительность

        verticalRotation -= lookInput.y; // вращаем камеру по вертикали
        verticalRotation = Mathf.Clamp(verticalRotation, -15f, 15f);  // мин/макс значения  для движения камеры по вертикали

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0); //выставляем персонажа по движению камеры
        transform.Rotate(Vector3.up * lookInput.x); // вращаем камеру по горизонтали
    }

    void HandleMovement()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        isRunning = runAction.IsPressed();
        float currentSpeed = isRunning ? runSpeed : jogSpeed;
        Vector3 move = (transform.right * moveInput.x + transform.forward * moveInput.y) * currentSpeed;
        controller.Move(move * Time.deltaTime);   
    }
}