using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgents = 10;
    public float spawnRadius = 2.0f;

    void Start()
    {
        SpawnAgents();
    }

    void SpawnAgents()
    {
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 spawnPosition = transform.position + UnityEngine.Random.insideUnitSphere * spawnRadius;
            GameObject agent = Instantiate(agentPrefab, spawnPosition, Quaternion.identity);
        }
    }
}