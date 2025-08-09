using UnityEngine;

public class NouraCharMov : MonoBehaviour
{
    public float walk = 5f;

    public float rotate = 50f;

    CharacterController cc;

    public float jumpHeight = 5f;

    Vector3 velocity;
    float gravity = -9.81f;
    bool isGrounded;


    Animator anim;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();   

    }



    private void Update()
    {


        isGrounded = cc.isGrounded;

        float leftRight = Input.GetAxis("Horizontal");
        float frontBack = Input.GetAxis("Vertical");


        // to rotate the character  
        if (leftRight != 0 || frontBack != 0)
        {
            Vector3 direction = new Vector3(leftRight, 0f, frontBack).normalized;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotate * Time.deltaTime);
        }
        // to play the animation    
        if (anim != null)
        {
            if (leftRight != 0 || frontBack != 0)
            {
                anim.SetBool("walk", true); 

            }
            else
            {
                anim.SetBool("walk", false);    
            }
        }       

        Vector3 forward = transform.forward * frontBack * walk * Time.deltaTime;

        cc.Move(forward);


        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
            // to always stick to the ground
        }
        Jumpping();
    }

    void Jumpping()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            //we apply the velocity to the y
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
        //  to always apply the gravity
        velocity.y += gravity * Time.deltaTime;

        // add the y move to the cc
        cc.Move(velocity * Time.deltaTime);
    }

}













