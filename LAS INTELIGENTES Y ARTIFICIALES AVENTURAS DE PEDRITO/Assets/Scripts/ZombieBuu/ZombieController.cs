using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    WANDER,
    CHASING
}

public class ZombieController : MonoBehaviour
{
    public ZombieManager manager;
    public NavMeshAgent agent;
    public State state;

    public float CoolDown = 2f;
    [SerializeField] private float CD_timer = 0.0f;
    [SerializeField] private float mapbounds = 150f;

    [SerializeField] private Vector3 destination = Vector3.zero;

    private void FollowPlayer() { state = State.CHASING; Debug.Log("Following player"); }

    private void Wander(float offset, float radius) 
    {
        Vector3 localTarget;

        if (CD_timer >= CoolDown && UnityEngine.Random.Range(0, 10) == 0)
        {
            localTarget = UnityEngine.Random.insideUnitCircle * radius;
            localTarget += new Vector3(0, 0, offset);

            destination = transform.TransformPoint(localTarget);
            destination.y = 0.0f;
            CD_timer = 0.0f;
        }

        if (destination.x > mapbounds)  { destination.x -= (destination.x / 2); }
        if (destination.x < -mapbounds) { destination.x -= (destination.x / 2); }
        if (destination.z > mapbounds)  { destination.z -= (destination.x / 2); }
        if (destination.z < -mapbounds) { destination.z -= (destination.x / 2); }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.WANDER;
    }

    void Update()
    {
        CD_timer += Time.deltaTime;

        //detectar player

        if (Input.GetKeyDown(KeyCode.Space)) FollowPlayer();

        if(state == State.WANDER) { Wander(15, 15); }
        if(state == State.CHASING) { destination = Vector3.zero; }

        agent.SetDestination(destination);
    }
}
