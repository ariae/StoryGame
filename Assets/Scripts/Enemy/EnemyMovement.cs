using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player; //Non public because of instantiating enemies rather than being static in scene
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        //Debug.Log("EnemyMovement - void Awake works.");
    }


    void Update () //Not fixedupdate because enemies are not in sync with physics (performance?) (Navmesh)
    {
        if(nav.enabled == true)
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
                //Debug.Log("EnemyMovement - player.position is lost. Disabling Nav.");
            }
        }
        
    }
}
