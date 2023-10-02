using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    [SerializeField] float maxSightDistance = 10f;
    [SerializeField] float hearingDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player is in sight, chase
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.position - transform.position, out hit, maxSightDistance)){
            if(hit.collider.CompareTag("Player")){
                        agent.SetDestination(player.position);

            }
        }

        //if player makes noise, chase
        //NEEDS TO BE INVOKED BY PLAYER STILL, will chase no matter sound atm
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer <= hearingDistance){
            agent.SetDestination(player.position);
        }
    }
}
