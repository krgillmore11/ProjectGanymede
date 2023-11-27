using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private EnemyManager em;
    public PlayerManager pm;
    [SerializeField] float maxSightDistance = 10f;
    [SerializeField] float hearingDistance = 5f;
    [SerializeField] float damageDistance = 5f;
    [SerializeField] float stoppingDistance = 3f;

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

        //if player is in sight, chase
            RaycastHit hit;
            if(Physics.Raycast(transform.position, player.position - transform.position, out hit, maxSightDistance)){
                if(hit.collider.CompareTag("Player")){
                    agent.SetDestination(player.position);
                }
            }
        

        //if player makes noise, chase
        //NEEDS TO BE INVOKED BY PLAYER STILL, will chase no matter sound atm
        if(distanceToPlayer <= hearingDistance){
            agent.SetDestination(player.position);
        }

        if(distanceToPlayer <= damageDistance){
            pm.TakeDamage(em.getDamage());
        }
    }
}
