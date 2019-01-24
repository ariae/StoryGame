using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float power = 1f;
    public float force = 2f;
    private float multiplyForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("Mouse Down");
            power += Time.deltaTime;
            multiplyForce += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Mouse Up");
            ThrowGrenade();
            power = 1;
            multiplyForce = force;
        }
    }


    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power * multiplyForce, ForceMode.VelocityChange);
    }
}