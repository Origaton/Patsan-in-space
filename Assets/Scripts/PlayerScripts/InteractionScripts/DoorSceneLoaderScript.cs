using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class DoorSceneLoaderScript : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask loadSceneLayer;

    private PlayerInput playerInput;
    private InputAction useAction;

    [SerializeField] private UnityEvent onSceneLoad;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        useAction = playerInput.actions["Use"];
    }

    void OnEnable()
    {
        useAction.performed += CheckSceneLoad;
    }

    void OnDisable()
    {
        useAction.performed -= CheckSceneLoad;
    }

    private void CheckSceneLoad(InputAction.CallbackContext ctx)
    {
        if (this == null) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance, loadSceneLayer);
        if (hitColliders.Length > 0)
        {
            onSceneLoad?.Invoke();
        }
    }
}