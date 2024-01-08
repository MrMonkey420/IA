using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public SphereCollider bounds; //Used to check in the editor the bounds of the group

    public int numFishes;

    public float neighbourDistance;
    public float minDistance;
    public float maxSpeed = 5.0f;
    public float minSpeed = 1.0f;
    public float rotSpeed = 2.0f;

    [SerializeField] private float swimBounds = 10f;

    public Vector3 centre;
    [SerializeField] private Vector3 targetPos = Vector3.zero;

    public GameObject[] Fishes;
    
    //------------------------------------------------------------
    private void CalculateCentre()
    {
        Vector3 positions = new Vector3();

        for (int i = 0; i < numFishes; i++)
        {
            positions += Fishes[i].transform.position;
        }

        centre = new Vector3(positions.x / numFishes, positions.y / numFishes, positions.z / numFishes);
    }

    //------------------------------------------------------------
    void Start()
    {
        Fishes = new GameObject[numFishes];
        bounds = GetComponent<SphereCollider>();
        bounds.radius = swimBounds; 

        for (int i = 0; i < numFishes; ++i)
        {
            Vector3 pos = this.transform.position + UnityEngine.Random.insideUnitSphere * 10; // Random position
            Vector3 randomize = UnityEngine.Random.insideUnitSphere; // Random vector direction

            Fishes[i] = Instantiate(fishPrefab, pos, Quaternion.identity);
            Fishes[i].GetComponent<BoidBehavior>().myManager = this;
        }
    }
    void Update()
    {
       CalculateCentre();
       //transform.position = centre;

       if(UnityEngine.Random.Range(0, 10) < 1)
       {
            targetPos = transform.position + new Vector3(UnityEngine.Random.Range(-swimBounds, + swimBounds),
                UnityEngine.Random.Range(-swimBounds, +swimBounds), UnityEngine.Random.Range(-swimBounds, +swimBounds));
       }
    }
}