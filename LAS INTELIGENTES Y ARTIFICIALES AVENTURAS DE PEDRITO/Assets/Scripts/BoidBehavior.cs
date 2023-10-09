using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    // Flocking parameters
    public float neighbourDistance = 3.0f;
    public float speed = 2.0f;
    public float rotationSpeed = 4.0f;

    // Random behavior parameters
    public float randomForce = 0.5f;
    public float randomInterval = 2.0f;

    // Leader behavior (optional)
    public GameObject leader;
    public float followDistance = 5.0f;
    public float followForce = 1.0f;

    private Vector3 direction;
    private Vector3 cohesion;
    private Vector3 alignment;
    private Vector3 separation;
    private float randomTimer;

    void Start()
    {
        randomTimer = UnityEngine.Random.Range(0, randomInterval);
    }

    void Update()
    {
        Flock();
        RandomBehavior();
        FollowLeader();
        Move();
    }

    void Flock()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int numNeighbors = 0;

        foreach (GameObject otherBee in GameObject.FindGameObjectsWithTag("Bee"))
        {
            if (otherBee != gameObject)
            {
                float distance = Vector3.Distance(transform.position, otherBee.transform.position);

                if (distance < neighbourDistance)
                {
                    numNeighbors++;

                    // Cohesion: Move towards the center of mass of neighbors.
                    cohesion += otherBee.transform.position;

                    // Alignment: Align with the average heading of neighbors.
                    alignment += otherBee.GetComponent<BoidBehavior>().direction;

                    // Separation: Avoid crowding neighbors.
                    separation += (transform.position - otherBee.transform.position) / (distance * distance);
                }
            }
        }

        if (numNeighbors > 0)
        {
            cohesion /= numNeighbors;
            alignment /= numNeighbors;
            separation /= numNeighbors;

            // Calculate the final direction as a combination of the three behaviors.
            direction = cohesion + alignment + separation;
        }
    }

    void RandomBehavior()
    {
        // Add random behavior to the bee's movement.
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0)
        {
            // Generate a random force.
            Vector3 randomForceVector = UnityEngine.Random.insideUnitSphere * randomForce;
            direction += randomForceVector.normalized;
            randomTimer = UnityEngine.Random.Range(0, randomInterval);
        }
    }

    private void FollowLeader()
    {
        if (leader != null)
        {
            float distanceToLeader = Vector3.Distance(transform.position, leader.transform.position);

            if (distanceToLeader > followDistance)
            {
                // Calculate force to follow the leader.
                Vector3 followForceVector = (leader.transform.position - transform.position).normalized * followForce;
                direction += followForceVector;
            }
        }
    }

    private void Move()
    {
        direction.Normalize();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}