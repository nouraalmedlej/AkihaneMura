using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform playerBody;       // اسحب اللاعب هنا

    [Header("Orbit")]
    public float mouseSens = 150f;     // 120–200 مناسب
    public float minPitch = -20f;
    public float maxPitch = 60f;

    [Header("Position")]
    public float distance = 5f;        // بعد الكاميرا عن اللاعب
    public float height = 2.2f;        // ارتفاع نقطة النظر
    public float followDamp = 12f;     // سلاسة التتبع

    float pitch = 0f;                  // دوران الكاميرا عموديًا

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // نبدأ من اتجاه اللاعب الحالي
        if (playerBody) transform.position = playerBody.position + Vector3.up * height - playerBody.forward * distance;
        Vector3 e = transform.eulerAngles;
        pitch = e.x;
    }

    void LateUpdate()
    {
        if (!playerBody) return;

        // إدخال الماوس (لكل ثانية)
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        // لفّ اللاعب أفقياً (Yaw)
        playerBody.Rotate(Vector3.up * mouseX);

        // لفّ الكاميرا عمودياً (Pitch)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // الموضع المرغوب خلف اللاعب
        Quaternion rot = Quaternion.Euler(pitch, playerBody.eulerAngles.y, 0f);
        Vector3 desiredPos = playerBody.position + Vector3.up * height - (rot * Vector3.forward) * distance;

        // تفادي اختراق الجدران (اختياري بسيط)
        if (Physics.Linecast(playerBody.position + Vector3.up * height, desiredPos, out RaycastHit hit))
            desiredPos = hit.point + hit.normal * 0.2f;

        // تحريك بسلاسة
        transform.position = Vector3.Lerp(transform.position, desiredPos, 1f - Mathf.Exp(-followDamp * Time.deltaTime));
        transform.rotation = rot;

        // نظرة خفيفة على أعلى جسم اللاعب
        transform.LookAt(playerBody.position + Vector3.up * 1.5f);
    }
}




