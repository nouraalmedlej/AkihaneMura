using UnityEngine;

public class capsulePlayer : MonoBehaviour
{
    public float speed = 10f;



    Rigidbody rb;

    public string nextLevel;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float leftRigt = Input.GetAxis("Horizontal");

        float frontBack = Input.GetAxis("Vertical");


        Vector3 move = new Vector3(leftRigt * speed * Time.deltaTime, frontBack * speed * Time.deltaTime);

        Vector3 movement = new Vector3(leftRigt, 0f, frontBack) * speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }
}
