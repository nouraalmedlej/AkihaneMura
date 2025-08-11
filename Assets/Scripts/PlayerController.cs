using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 8f; // أعلى = التفاف أسرع
    public Transform cameraTransform; // حطي Main Camera هنا

    [Header("Jump/Gravity")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Animator anim;
    private Vector3 velocity;
    private float turnSmoothVelocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        if (anim) anim.applyRootMotion = false;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // اتجاه الإدخال
        Vector3 inputDir = new Vector3(x, 0f, z).normalized;

        // حركة ودوران نسبيان لاتجاه الكاميرا
        if (inputDir.sqrMagnitude > 0.001f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg;
            if (cameraTransform) targetAngle += cameraTransform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                1f / Mathf.Max(0.001f, rotationSpeed) // زمن نعومة قصير = التفاف أسرع
            );

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }

        // أنيميشن سرعة أفقية
        if (anim)
        {
            // سرعة أفقية فعلية بدون محور Y
            Vector3 horizVel = controller.velocity; horizVel.y = 0f;
            anim.SetFloat("Speed", horizVel.magnitude);
        }

        // قفز + تأريض
        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            if (anim) anim.SetTrigger("Jump");
        }

        // جاذبية
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}


