using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 700f;
    private CharacterController controller;
    private Vector3 velocity;





    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       

        //Vector3 move = transform.forward * z * moveSpeed * Time.deltaTime;
        //controller.Move(move);
       Vector3 move = new Vector3(x, 0, z);
        controller.Move(move * 5 * Time.deltaTime);
        float angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        // gravity
        if (!controller.isGrounded)
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        else
        {
            velocity.y = -2f;
        }
        //rotation with camera machine
        if (Camera.main != null)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0; // Ignore vertical component
            cameraForward.Normalize();
            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0; // Ignore vertical component
            cameraRight.Normalize();
            //Vector3 moveDirection = (cameraForward * z + cameraRight * x).normalized;
            //controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }   

    }
}



