using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public GameObject[] Zombies;
    public GameObject[] Spawners;

    public int numZombies;
    public float velocity;

    public void Broadcast()
    {
        BroadcastMessage("FollowPlayer");
    }

    void Start()
    {
        Zombies = new GameObject[numZombies];

        for(int i = 0; i < numZombies; i++)
        {
            Vector3 pos = Spawners[i].transform.position;
            Zombies[i] = Instantiate(ZombiePrefab, pos, Quaternion.identity);
            Zombies[i].GetComponent<ZombieController>().manager = this;
            Zombies[i].GetComponent<AIVision>().GetComponent<ZombieController>().manager = this;
            Zombies[i].transform.parent = this.transform;
        }
    }

    void Update()
    {
        
    }
}
