using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numFishes;

    public float maxSpeed = 5.0f;
    public float minSpeed = 1.0f;

    public float neighbourDistance = 2.0f;
    public float rotationSpeed = 2.0f;
    public bool Randomize = false;
    [SerializeField] private Vector3 center;

    public GameObject[] Fishes;
    
    //------------------------------------------------------------
    private void CalculateCenter()
    {
        Vector3 positions = new Vector3();

        for (int i = 0; i < numFishes; i++)
        {
            positions += Fishes[i].transform.position;
        }

        center = new Vector3(positions.x / numFishes, positions.y / numFishes, positions.z / numFishes);
    }

    //------------------------------------------------------------
    void Start()
    {
        Fishes = new GameObject[numFishes];

        for (int i = 0; i < numFishes; ++i)
        {
            Vector3 pos = this.transform.position + UnityEngine.Random.insideUnitSphere * 10; // Random position
            Vector3 randomize = UnityEngine.Random.insideUnitSphere; // Random vector direction

            // Ensure that the fish are at a consistent depth (e.g., swimming near the surface)
            pos.y = Mathf.Clamp(pos.y, 1.0f, 10.0f);

            Fishes[i] = Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize));
            Fishes[i].GetComponent<BoidBehavior>().myManager = this;
        }
    }
    void Update()
    {
       //CalculateCenter();
       //transform.position = center;
    }
}