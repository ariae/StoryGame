using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f; //Sink through the floor with this speed
    public int scoreValue = 10; //Enemy score value
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles; //Hitparticle child reference
    BoxCollider boxCollider;
    //CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> (); //Finds component in child hierarchy
        boxCollider = GetComponent<BoxCollider>();
        //capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
        //Debug.Log("EnemyHealth - void Awake works.");
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); //Moves transform downwards when it's time to sink (Translate doesn't use physics)
            //Debug.Log("EnemyHealth - Enemy is sinking.");
            isSinking = false;

        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint) //Hitpoint will be WHERE enemy has been hit(Vector3), for the particle system's origin point
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint; //Moves particle child to hitpoint
        hitParticles.Play();
        //Debug.Log("EnemyHealth - Enemy took damage.");

        if (currentHealth <= 0)
        {
            Death ();
 
        }
    }


    void Death ()
    {
        isDead = true;

        //capsuleCollider.isTrigger = true; //Removes collider so the player doesn't collide with corpses
        boxCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip; //changes the audio to deathClip and plays it below
        enemyAudio.Play ();
        //Debug.Log("EnemyHealth - Enemy is dying.");
        StartSinking();
    }

    //Efter void Death () så slutar scriptet fungera?

    public void StartSinking ()
    {
        //Debug.Log("3. Gathering NavMeshAgent/Rigidbody and setting isSinking to true.");
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; //disables the navmesh agent COMPONENT and disables it
        GetComponent <Rigidbody> ().isKinematic = true; //changes object into kinematic so it doesn't recalculate static geometry for Rigidbody
        isSinking = true;
        ScoreManager.score += scoreValue; //adds value to the ScoreManager script integer "score"
        Destroy (gameObject, 2f); //Destroys gameObject after 2 seconds
    }
}
