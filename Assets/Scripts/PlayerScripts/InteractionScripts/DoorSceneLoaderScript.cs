using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class DoorSceneLoaderScript : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionWithDoorDistance;
    [SerializeField] private LayerMask loadSceneLayer;

    private PlayerInput playerInput;
    private InputAction useAction;

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

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionWithDoorDistance, loadSceneLayer);
        if (hitColliders.Length > 0)
        {
            string doorName = hitColliders[0].gameObject.name;
            GlobalEventManager.PressedChengeLocation(doorName);
        }
    }

    //Просто область взаимодействия
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, interactionWithDoorDistance);
    }
}
