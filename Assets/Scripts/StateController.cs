using UnityEngine;
using UnityEngine.InputSystem;

public class StateController : MonoBehaviour
{
    [SerializeField] private StateController characterState;
    
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;

    private Animator animator;
    private State currentState;
    
    private void Start() 
    {
        animator = GetComponent<Animator>();
        currentState = new IdleState(animator, 1f);
        currentState.Enter();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        runAction = playerInput.actions["Run"];
    }

    void Update()
    {
        if (moveAction.ReadValue<Vector2>().magnitude > 0.001f == true)
        {
            if (runAction.IsPressed())
            {
                currentState.Exit();
                currentState = new RunState(animator, 1f);
                currentState.Enter();
                
            }
            else
            {
                currentState.Exit();
                currentState = new JogState(animator, 1f);
                currentState.Enter();
            }
        }
        else
        {
            currentState.Exit();
            currentState = new IdleState(animator, 1f);
            currentState.Enter();
        }
    }
}