using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    public NavMeshAgent agent;
    [SerializeField] private bool control = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) control = !control;

        if (control)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray movepos = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(movepos, out var hitInfo))
                {
                    agent.SetDestination(hitInfo.point);
                }
            }
        }

    }
}
