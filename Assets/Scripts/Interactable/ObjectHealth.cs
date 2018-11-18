using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public int startingHealth = 1000;
    public int currentHealth;
    public AudioClip brokenClip;

    Animator anim;
    AudioSource objectAudio;
    ParticleSystem hitParticles; //Hitparticle child reference
    BoxCollider boxCollider;
    bool isBroken;

    void Awake()
    {
        Debug.Log("ObjectHealth 1 : void Awake starts");
        anim = GetComponent<Animator>();
        objectAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>(); //Finds component in child hierarchy
        boxCollider = GetComponentInChildren<BoxCollider>();

        currentHealth = startingHealth;
    }

    //TakeDamage doesn't happen||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    public void TakeDamage(int amount, Vector3 hitPoint) //Hitpoint will be WHERE enemy has been hit(Vector3), for the particle system's origin point
    {
        Debug.Log("ObjectHealth 2 : void TakeDamage script starts"); //No it doesn't :(

        if (isBroken)
            return;

        objectAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint; //Moves particle child to hitpoint
        hitParticles.Play();
        Debug.Log("ObjectHealth 3 : TakeDamage script finished and Door took damage.");

        if (currentHealth <= 0)
        {
            Death();
        }
    }
    //TakeDamage doesn't happen||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

    void Death()
    {
        Debug.Log("ObjectHealth 4 : void Death starts");
        isBroken = true;

        boxCollider.isTrigger = true; //Removes collider so the player doesn't collide (Probably will remove box collider on walls as well...)

        anim.SetTrigger("IsBroken");

        objectAudio.clip = brokenClip; //changes the audio to brokenClip and plays it below
        objectAudio.Play();
        Debug.Log("ObjectHealth 5 : Object is breaking.");
    }
}

