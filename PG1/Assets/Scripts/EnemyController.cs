using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public PlayerController pc;
    private EnemyManager em;
    public PlayerManager pm;
    public bool chasing = false;
    public bool wandering = true;
    public bool isAttacking;
    float distanceToPlayer;
    bool chaseSoundStarted = false;
    [SerializeField] float maxSightDistance = 20f;
    [SerializeField] float hearingDistance = 5f;
    [SerializeField] float damageDistance = 4f;
    [SerializeField] float stoppingDistance = 2f;
    [SerializeField] float chaseEndDistance = 20f;
    [SerializeField] float wanderDistance = 10f;
    [SerializeField] float wanderBreak = 5f;
    [SerializeField] float attackBreak = 5f;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip chaseSound;
    [SerializeField] GameObject attackParticle;


    // Start is called before the first frame update
    void Start()
    {
        em = GetComponent<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (chasing){
            Chase();
        }
        else if (wandering){
            StartWander();
        }
        if (chasing && distanceToPlayer > chaseEndDistance){
            StopChase();
        }

        //if player is in sight, chase
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxSightDistance)){
                if(hit.collider.CompareTag("Player")){
                    StartChase();
                }
            }
    
        //if player makes noise, chase
        if(distanceToPlayer <= hearingDistance && pc.IsSprinting()){
            StartChase();
        }
        if(distanceToPlayer <= damageDistance && chasing && !isAttacking){
            Debug.Log("Made it to start couroutine");
            StartCoroutine(AttackBreak());
        }
    }
    void Chase(){
        agent.SetDestination(player.position);
    }
    public void StartChase(){
        chasing = true;
        wandering = false;
        if (!chaseSoundStarted){
            AudioSource.PlayClipAtPoint(chaseSound, transform.position);
            chaseSoundStarted = true;
        }
        //Debug.Log("Chasing");
    }
    void StopChase(){
        chasing = false;
        wandering = true;
        chaseSoundStarted = false;
        Debug.Log("Stopped Chase");
    }
    void StartWander(){
        //wandering = true;
        chasing = false;
        StartCoroutine(WanderBreak());
        Debug.Log("StartWander wander routeine");
    }

    IEnumerator WanderBreak(){
        wandering  = false;
            Vector3 randomPoint = Random.insideUnitSphere * wanderDistance;
            randomPoint.y = 0;
            Vector3 destination = transform.position + randomPoint;
            agent.SetDestination(destination);
            //Debug.Log("Wandering");
            yield return new WaitForSeconds(wanderBreak);
                wandering  = true;;

    }

    IEnumerator AttackBreak(){
        isAttacking = true;
        while (chasing && distanceToPlayer <= damageDistance){
        Debug.Log("Made it");
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
        Instantiate(attackParticle, transform.position, Quaternion.identity);
        pm.TakeDamage(em.getDamage());
        Debug.Log("p took dam");
        yield return new WaitForSeconds(attackBreak);
        }
        isAttacking = false;
    }
}
