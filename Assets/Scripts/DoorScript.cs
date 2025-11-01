using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Transform))]
public class DoorScript : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;
    [SerializeField] private float interactionDistance;
    private PlayerInput playerInput;
    private InputAction useAction;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        useAction = playerInput.actions["Use"];

        useAction.performed += ctx =>
        {
            if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= interactionDistance)
            {
                LoadScene();
                Debug.Log("Click");
            }
        };
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
