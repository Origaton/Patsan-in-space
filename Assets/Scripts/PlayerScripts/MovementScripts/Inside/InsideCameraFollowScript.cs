using UnityEngine;

[RequireComponent(typeof(Transform))]
public class InsideCameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [Header("Movement")]
    [SerializeField] private float followSpeed;
    [SerializeField] private float parallaxFactor;

    [Header("Room Boundaries")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 initialOffset;

    private void Awake()
    {
        // Вычисляем начальный offset между камерой и игроком
        initialOffset = transform.position - targetTransform.position;

        //Блокировка курсора при запуске игры. Просто для удобства
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        // Желаемая позиция: позиция игрока + начальный offset, умноженный на параллакс фактор
        Vector3 desiredPosition = targetTransform.position + initialOffset * parallaxFactor;

        // Ограничиваем позицию камеры границами комнаты
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, minZ, maxZ);

        // Плавное движение к желаемой позиции
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref cameraVelocity, 1f / followSpeed);
    }
}
