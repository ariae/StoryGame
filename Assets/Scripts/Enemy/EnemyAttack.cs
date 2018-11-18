using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim; //Reference to external animations
    GameObject player; //Reference to external player
    PlayerHealth playerHealth; //Reference to external player health
    EnemyHealth enemyHealth; //Reference to external enemy health
    bool playerInRange; //Triggers once player is in range of attack
    float timer; //Timer for everything to sync


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); //Finds and stores player (tagged Player) and stores in "player"
        playerHealth = player.GetComponent <PlayerHealth> (); //Finds playerHealth and stores in "playerHealth"
        enemyHealth = GetComponent<EnemyHealth>(); //Gets external script EnemyHealth
        anim = GetComponent <Animator> (); //Finds reference for animation
        //Debug.Log("EnemyAttack - void Awake works.");
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            //Debug.Log("EnemyAttack - Player in range.");
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
            //Debug.Log("EnemyAttack - Player out of range.");
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
            //Debug.Log("EnemyAttack - Enemy attacking.");
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead"); //Transitions from moving to idle state (player is dead)
            //Debug.Log("EnemyAttack - Player is dead.");
        }
    }


    void Attack ()
    {
        timer = 0f; //Resets timer for attack interval

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage); //Do (attackDamage) to player until health is <=0
            //Debug.Log("EnemyAttack - Player takes damage.");
        }
    }
}
