using UnityEngine;
using UnityEngine.AI;

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
        if (Input.GetMouseButtonDown(0) && firstClick == true)
        {
            RaycastHit hit;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out hit, 100))
            {
                firstClick = false;
                SpawnCompanions(hit.point);
                MoveLeader(hit.point);
            }
        }
        else if (Input.GetMouseButtonDown(0) && firstClick == false)
        {
            RaycastHit hit;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out hit, 100))
            {
                MoveLeader(hit.point);
            }
        }
    }

    void MoveLeader(Vector3 destination)
    {
        leaderAgent.destination = destination;
        isMoving = true;
        if (companions != null)
        {
            foreach (GameObject companion in companions)
            {
                if (companion != null)
                {
                    NavMeshAgent companionAgent = companion.GetComponent<NavMeshAgent>();
                    companionAgent.enabled = true;
                    companionAgent.destination = destination;
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
            Vector3 offset = Quaternion.AngleAxis(45 * i, Vector3.up) * leaderToClick.normalized * 2f;
            Vector3 spawnPos = transform.position + offset;
            GameObject newCop = Instantiate(copPrefab, spawnPos, rotation);
            newCop.GetComponent<NavMeshAgent>().enabled = false;
            companions[i] = newCop;
        }
    }
}