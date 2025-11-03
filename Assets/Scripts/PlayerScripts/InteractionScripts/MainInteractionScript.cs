using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class MainInteractionScript : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactableLayer;

    private PlayerInput playerInput;
    private InputAction useAction;

    [SerializeField] private UnityEvent onInteracted;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        useAction = playerInput.actions["Use"];

        useAction.performed += CheckInteraction;
    }

    private void CheckInteraction(InputAction.CallbackContext ctx)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance, interactableLayer);
        if (hitColliders.Length > 0)
        {
            onInteracted?.Invoke();
        }
    }

    //Просто область взаимодействия 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, interactionDistance);
    }
}