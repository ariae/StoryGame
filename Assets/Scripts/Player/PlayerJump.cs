using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity = 10f;
    private string grounded = "y";

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (grounded == "y"))
        {
            rb.velocity = Vector3.up * jumpVelocity;
            grounded = "n";
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (GetComponent<Rigidbody>().velocity.y == 0)
        {
            grounded = "y";
        }
    }
}