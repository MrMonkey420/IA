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
    
    NavMeshAgent agent;

    private void Patroling()
    {
        patrolWP = (patrolWP + 1) % waypoints.Length;
        agent.SetDestination(waypoints[patrolWP].transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolWP = UnityEngine.Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 2f) Patroling();
    }
}
