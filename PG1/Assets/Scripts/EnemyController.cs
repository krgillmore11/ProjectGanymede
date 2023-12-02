using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public PlayerController pc;
    private EnemyManager em;
    public PlayerManager pm;
    bool chasing = false;
    bool wandering = true;
    bool isAttacking;
    [SerializeField] float maxSightDistance = 20f;
    [SerializeField] float hearingDistance = 5f;
    [SerializeField] float damageDistance = 4f;
    [SerializeField] float stoppingDistance = 2f;
    [SerializeField] float chaseEndDistance = 20f;
    [SerializeField] float wanderDistance = 10f;
    [SerializeField] float wanderBreak = 5f;
    [SerializeField] float attackBreak = 5f;


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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (chasing){
            Chase();
        }
        else if(!wandering){
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
            StartCoroutine(AttackBreak());
        }
    }
    void Chase(){
        agent.SetDestination(player.position);
    }
    public void StartChase(){
        chasing = true;
        wandering = false;
        Debug.Log("Chasing");
    }
    void StopChase(){
        chasing = false;
        wandering = false;
        Debug.Log("Stopped Chase");
    }
    void StartWander(){
        wandering = true;
        chasing = false;
        StartCoroutine(WanderBreak());
        
    }

    IEnumerator WanderBreak(){
        while (wandering){
            Vector3 randomPoint = Random.insideUnitSphere * wanderDistance;
            randomPoint.y = 0;
            Vector3 destination = transform.position + randomPoint;
            agent.SetDestination(destination);
            Debug.Log("Wandering");
            yield return new WaitForSeconds(wanderBreak);
        }
    }

    IEnumerator AttackBreak(){
        isAttacking = true;
        while (chasing){
        pm.TakeDamage(em.getDamage());
        Debug.Log("p took dam");
        yield return new WaitForSeconds(attackBreak);
        }
        isAttacking = false;
    }
}
