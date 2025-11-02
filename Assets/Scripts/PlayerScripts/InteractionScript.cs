using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class InteractionScript : MonoBehaviour
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

        useAction.performed += ctx => CheckInteraction();
    }

    private void CheckInteraction()
    {
        //Нужно добавить взаимодействие на небольшом расстоянии
        onInteracted.Invoke();
    }

    // Просто визуализация области взаимодействия
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
