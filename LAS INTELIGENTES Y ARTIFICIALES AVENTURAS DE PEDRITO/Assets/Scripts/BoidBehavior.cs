using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public float cohesionRadius = 3.0f;
    public float separationDistance = 1.0f;
    public float alignmentStrength = 1.0f;
    public float randomnessFactor = 0.2f;

    private Vector3 velocity; // Add the velocity field here

    void Start()
    {
        // Initialize the velocity
        velocity = transform.forward * speed;
    }

    void Update()
    {
        // Get nearby Boids within cohesionRadius
        Collider[] nearbyBoids = Physics.OverlapSphere(transform.position, cohesionRadius);

        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;

        foreach (var boid in nearbyBoids)
        {
            if (boid != this) // Exclude the current Boid
            {
                Vector3 toBoid = boid.transform.position - transform.position;

                // Cohesion
                cohesion += toBoid;

                // Separation
                if (toBoid.magnitude < separationDistance)
                    separation -= toBoid.normalized / toBoid.magnitude;

                // Alignment
                alignment += boid.GetComponent<BoidBehavior>().velocity;
            }
        }

        // Apply randomness
        Vector3 randomness = new Vector3(UnityEngine.Random.Range(-randomnessFactor, randomnessFactor), 0, UnityEngine.Random.Range(-randomnessFactor, randomnessFactor));

        // Calculate new velocity
        velocity = (cohesion + separation + alignment + randomness).normalized * speed;

        // Update Boid's position
        transform.position += velocity * Time.deltaTime;
    }
}
