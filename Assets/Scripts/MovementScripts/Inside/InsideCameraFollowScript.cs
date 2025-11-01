using UnityEngine;

[RequireComponent(typeof(Transform))]
public class InsideCameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [Header("Deadzone")]
    [SerializeField] private float deadzoneWidth;
    [SerializeField] private float deadzoneHeight;
    [SerializeField] private float deadzoneOffset;

    [Header("Movement")]
    [SerializeField] private float followSpeed;
    [SerializeField] private float parallaxFactor;

    private Vector3 cameraVelocity = Vector3.zero;
    private Vector3 lastTargetPosition;
    private Vector3 targetPos;
    private Vector3 cameraPos;

    private void Awake()
    {
        lastTargetPosition = targetTransform.position;
        
        //Блокировка курсора при запуске игры. Просто для удобства
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        targetPos = targetTransform.position;
        cameraPos = transform.position;

        // Центр deadzone смещен по направлению взгляда камеры
        Vector3 deadzoneCenter = cameraPos + transform.forward * deadzoneOffset;

        // Проверяем, вышел ли игрок из deadzone
        bool outOfDeadzoneX = Mathf.Abs(targetPos.x - deadzoneCenter.x) > deadzoneWidth / 2f;
        bool outOfDeadzoneZ = Mathf.Abs(targetPos.z - deadzoneCenter.z) > deadzoneHeight / 2f;

        Vector3 desiredPosition = cameraPos;

        // Камера следует по X и Z, позволяя диагональное движение
        if (outOfDeadzoneX)
        {
            desiredPosition.x = targetPos.x;
        }
        if (outOfDeadzoneZ)
        {
            desiredPosition.z = targetPos.z;
        }

        // Y остается тем же (камера не следует по вертикали)
        desiredPosition.y = cameraPos.y;

        // Эффект параллакса: камера следует не полностью, а с коэффициентом
        Vector3 parallaxOffset = (desiredPosition - cameraPos) * parallaxFactor;
        desiredPosition = cameraPos + parallaxOffset;

        // Плавное движение
        transform.position = Vector3.SmoothDamp(cameraPos, desiredPosition, ref cameraVelocity, 1f / followSpeed);

        lastTargetPosition = targetPos;
    }
}
