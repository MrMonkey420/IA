using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public GameObject[] waypoints;
    [SerializeField] private int patrolWP;
    
    public NavMeshAgent agenthost;

    private void Patroling()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        agenthost.SetDestination(waypoints[patrolWP].transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        agenthost = GetComponent<NavMeshAgent>();
        patrolWP = UnityEngine.Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agenthost.pathPending && agenthost.remainingDistance < 1f) Patroling();
    }
}
