using UnityEngine;
using UnityEngine.UI; //UI Stuff
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; // Sets default starting health in Unity UI
    public int currentHealth; // Stores current health
    public Slider healthSlider; //UI Stuff, Sets health slider in Unity UI (For Overlay)
    public Image damageImage; //UI Stuff, Sets damage image in Unity UI (For Overlay)
    public AudioClip deathClip; // Sets death sound file "deathClip" in Unity UI
    public float flashSpeed = 5f; //UI Stuff, sets speed of Image flashing
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //UI Stuff, sets the Image to a pure red with alpha of 0.1/1.0


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement; //1. Type(name of the script) 2. variable name
    PlayerShooting playerShooting; //1. Type(name of the script) 2. variable name
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> (); //Component on children (gunbarrel end)
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount) //Public because it interacts and receives bool etc from other scripts
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead) //Death
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true; //Player is dead

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die"); //Triggers death animation
        playerAudio.clip = deathClip; // Sets death sound file "deathClip" in Unity UI
        playerAudio.Play (); //Oneshot Death audio clip

        playerMovement.enabled = false; //Stops external "playerMovement" script
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
