using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator anim; //Reference to external animations
    GameObject player; //Reference to external player
    GameObject enemy; //Reference to external enemy
    //ParticleSystem hitParticles; //Hitparticle child reference                                                                    ENEMYHEALTHSCRIPT
    //BoxCollider boxCollider;                                                                                                      ENEMYHEALTHSCRIPT

    bool open = false;
    int objectsAtDoor;

    void Awake()
    {
        anim = GetComponent<Animator>(); //Finds reference for animation
        //hitParticles = GetComponentInChildren<ParticleSystem>(); //Finds component in child hierarchy                             ENEMYHEALTHSCRIPT
        //boxCollider = GetComponent<BoxCollider>();                                                                                ENEMYHEALTHSCRIPT
    }

    void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Finds and stores player (tagged Player) and stores in "player"
        enemy = GameObject.FindGameObjectWithTag("Enemy"); //Finds and stores player (tagged Player) and stores in "player"

        if (objectsAtDoor == 0)
        {
            open = true;
            anim.SetBool("IsOpen", open);
            objectsAtDoor = 1;
            Debug.Log("ObjectsAtDoor : 1");
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (objectsAtDoor == 1)
        {
            open = false;
            anim.SetBool("IsOpen", open);
            objectsAtDoor = 0;
            Debug.Log("ObjectsAtDoor : 0");
        }

    }
}