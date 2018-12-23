using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    bool shooting;
    Animator anim;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
    int rayEnemy;                                                                           //rayEnemy = Layer mask that hits Enemies and deals damage
    int rayObject;                                                                          //rayObject = Layer mask that breaks Line of gunLine (But don't register player direction raycast) NEED A TAG FOR BREAKABLE OBJECTS HERE

    void Awake ()
    {
        //Debug.Log("PlayerShooting 1 : void Awake starts");
        rayEnemy = LayerMask.GetMask ("Enemy");
        rayObject = LayerMask.GetMask("RayObject");                                         //!!!!!Need to fix raycast collission with walls
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        anim = GetComponentInParent<Animator>();
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            bool shooting = true; 
            anim.SetBool("IsShooting", shooting); 
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
            bool shooting = false; 
            anim.SetBool("IsShooting", shooting); 
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
                                                                                        //Debug.Log("PlayerShooting 2 : void Shoot starts");
        timer = 0f;

            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range))
            {
                if (Physics.Raycast(shootRay, out shootHit, range, rayEnemy))
                {

                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                    }
                    gunLine.SetPosition(1, shootHit.point);
                Debug.Log("PlayerShooting 3 : Enemy is hit.");
            }

            if (Physics.Raycast(shootRay, out shootHit, range, rayObject))
            {
                ObjectHealth objectHealth = shootHit.collider.GetComponent<ObjectHealth>();

                if (objectHealth != null)
                {
                    objectHealth.TakeDamage(damagePerShot, shootHit.point);
                }
                gunLine.SetPosition(1, shootHit.point);
                Debug.Log("PlayerShooting 4 : Wall/Object is hit.");
            }
        }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }

    }

}
