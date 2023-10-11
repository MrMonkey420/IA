using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowGhost : MonoBehaviour
{
    public NavMeshAgent ghost;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        agent.destination = ghost.transform.position;
    }
}
