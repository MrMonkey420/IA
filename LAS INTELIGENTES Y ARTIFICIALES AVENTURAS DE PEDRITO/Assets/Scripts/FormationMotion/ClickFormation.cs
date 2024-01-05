using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ClickFormation : MonoBehaviour
{
    public GameObject copPrefab; // Assign your "Cop" prefab in the inspector
    private NavMeshAgent leaderAgent;
    private GameObject[] companions;
    private bool isMoving = false;
    private bool firstClick = true;

    void Start()
    {
        leaderAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out hit, 100))
            {
                if (firstClick)
                {
                    SpawnCompanions(hit.point);
                    MoveLeader(hit.point);
                    firstClick = false;
                }
                else MoveLeader(hit.point);
            }
        }
    }

    void MoveLeader(Vector3 destination)
    {
        leaderAgent.destination = destination;
        isMoving = true;
        if (companions != null)
        {
            Vector3 leaderToClick = destination - transform.position;

            for (uint i = 0; i < companions.Length; i ++)
            {
                if (companions[i] != null)
                {
                    Vector3 offset = transform.rotation * Quaternion.AngleAxis(45 * i, Vector3.up) * destination.normalized * 2f;
                    
                    Vector3 compDestination = destination + offset;

                    NavMeshAgent companionAgent = companions[i].GetComponent<NavMeshAgent>();
                    companionAgent.enabled = true;
                    companionAgent.destination = compDestination;
                }
            }
        }
    }

    void SpawnCompanions(Vector3 clickPosition)
    {
        companions = new GameObject[8];
        Vector3 leaderToClick = clickPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(-leaderToClick.normalized, Vector3.up);

        for (int i = 0; i < companions.Length; i++)
        {
            Vector3 offset = Quaternion.AngleAxis(45 * i, Vector3.up) * clickPosition.normalized * 2f;
            Vector3 spawnPos = transform.position + offset;

            GameObject newCop = Instantiate(copPrefab, spawnPos, rotation);
            newCop.GetComponent<NavMeshAgent>().enabled = false;
            companions[i] = newCop;
        }
    }
}