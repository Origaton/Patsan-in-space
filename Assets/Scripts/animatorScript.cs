using UnityEngine;
using UnityEngine.InputSystem;

public class animatorScript : MonoBehaviour
{

    Animator animator;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction runAction;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        runAction = playerInput.actions["Run"];
    }

    void Update()
    {
       
        if (moveAction.ReadValue<Vector2>().magnitude > 0.1f == true)
        {
            if (runAction.IsPressed())
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);

        }

    }

}