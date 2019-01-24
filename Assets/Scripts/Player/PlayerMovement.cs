using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 6f;
    public float movementSprintSpeed = 1.5f;

    Vector3 movement;
    Animator anim;
    Rigidbody rb;
    int floorLayer;
    float camRayLength = 100f;

    void Awake()
    {
        floorLayer = LayerMask.GetMask("RayFloor");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Inputs
        float h = Input.GetAxisRaw("Horizontal");       // Positive and Negative X axis
        float v = Input.GetAxisRaw("Vertical");         // Positive and Negative Y axis
        //Functions
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * movementSpeed * movementSprintSpeed * Time.deltaTime;          //Time between each update call ( movement * speed Over 1 second
            rb.MovePosition(transform.position + movement);
            //Debug.Log("sprint has happened");
        }
        else
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * movementSpeed * Time.deltaTime;                            //Time between each update call ( movement * speed Over 1 second
            rb.MovePosition(transform.position + movement);
            //Debug.Log("sprint hasn't happened");
        }
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorLayer))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }


}
