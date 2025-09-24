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

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        runAction = playerInput.actions["run"];
    }

    void Update()
    {

    }
}