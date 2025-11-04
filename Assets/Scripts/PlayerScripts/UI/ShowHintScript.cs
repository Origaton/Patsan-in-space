using UnityEngine;
using TMPro;

[RequireComponent(typeof(Transform))]
public class ShowHintScript : MonoBehaviour //Мне все равно не нравится реализация, надо переделать когда-то
{
    [SerializeField] private float hintDistance;
    [SerializeField] private string interactionButton = "E"; //Поменять потом на кнопку из InputManager

    [Header("Related objects")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI hintField;
    [SerializeField] private LayerMask interactableLayer;

    void Awake()
    {
        if (hintField != null)
        {
            hintField.gameObject.SetActive(true);
            hintField.enabled = false;
        }
    }

    private void Update()
    {
        if (hintField == null)
        {
            Debug.Log("No text field!");
            return;
        }

        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, hintDistance, interactableLayer);
        bool found = false;
        foreach (Collider collider in hitColliders)
        {
            string hintMessage = HintDatabase.GetHintMessage(interactionButton, collider.gameObject.tag);
            if (hintMessage != "null")
            {
                hintField.text = hintMessage;
                hintField.enabled = true;
                found = true;
                break;
            }
        }
        if (!found)
        {
            hintField.enabled = false;
        }
    }
}
