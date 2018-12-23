using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float speedMultiplier = 1.5f;
   // public float jumpSpeed; //DERP

    //public float height = 0.5f;                   //1 Character height
    //public float heightPadding = 0.05f;           //1 Padding for unity error jitter
    //public LayerMask floorLayer;                  //1 Ground Mask
    //public float maxGroundAngle = 120;            //1 Maximum Angle of traversing
    //public bool debug;                            //1

    Vector3 movement;
    Vector3 jumping;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorLayer;
    float camRayLength = 100f;

    void Awake()
    {
        floorLayer = LayerMask.GetMask("RayFloor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Inputs
        float h = Input.GetAxisRaw("Horizontal");       // Positive and Negative X axis
        float v = Input.GetAxisRaw("Vertical");         // Positive and Negative Y axis
        bool s = Input.GetKeyDown("left shift");        //Sprint Key
        //Functions
        Move(h, v);
        Turning();
        Animating(h, v);
        Jumping(h, speed, v);    //DERP
    }

    void Move(float h, float v)
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * speedMultiplier * Time.deltaTime;          //Time between each update call ( movement * speed Over 1 second
            playerRigidbody.MovePosition(transform.position + movement);
            //Debug.Log("sprint has happened");
        }
        else
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;                            //Time between each update call ( movement * speed Over 1 second
            playerRigidbody.MovePosition(transform.position + movement);
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
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

    void Jumping(float h, float jumpSpeed, float v)
    {
        Debug.Log("01.Jumping starts");
        playerRigidbody.AddForce (movement * speed);

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("02. Space detected");
            jumpSpeed = 5.0f;
            Vector3 jump = new Vector3(0.0f, speed, 0.0f);
        }
    }
}
