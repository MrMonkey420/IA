using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float offset, radius, cooldown;
    [SerializeField] private float timeBeforeCD = 0.0f;
    [SerializeField] private float mapbounds = 19f;
    [SerializeField] private Vector3 localTarget, worldTarget = Vector3.zero;

    private NavMeshAgent agent;

    private void ChangeWorldTarget()
    {
        localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);

        worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeCD += Time.deltaTime;

        if (timeBeforeCD >= 1f && UnityEngine.Random.Range(0, 10) == 0)
        {
            ChangeWorldTarget(); 
            timeBeforeCD = 0.0f;
        }

        if(transform.position.x > mapbounds)  { worldTarget.x = -worldTarget.x; }
        if(transform.position.x < -mapbounds) { worldTarget.x = -worldTarget.x; }
        if(transform.position.z > mapbounds)  { worldTarget.z = -worldTarget.z; }
        if(transform.position.z < -mapbounds) { worldTarget.z = -worldTarget.z; }

        agent.SetDestination(worldTarget);
    }
}
