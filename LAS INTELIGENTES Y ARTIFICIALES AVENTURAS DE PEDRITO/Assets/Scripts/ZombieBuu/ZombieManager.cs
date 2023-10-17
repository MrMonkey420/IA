using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject ZombiePrefab;
    public GameObject player;
    public GameObject Spawners;

    public GameObject[] Zombies;

    [SerializeField] private int numZombies;

    public void Broadcast()
    {
        BroadcastMessage("FollowPlayer");
    }

    void Start()
    {
        numZombies = Spawners.transform.childCount;
        Zombies = new GameObject[numZombies];

        for (int i = 0; i < numZombies; i++)
        {
            Vector3 pos = Spawners.transform.GetChild(i).transform.position;
            Quaternion rot = UnityEngine.Random.rotation;

            Zombies[i] = Instantiate(ZombiePrefab, pos, rot);
            Zombies[i].GetComponent<ZombieController>().manager = this;
            Zombies[i].GetComponent<AIVision>().GetComponent<ZombieController>().manager = this;
            Zombies[i].GetComponent<ZombieController>().player = this.player;
            Zombies[i].transform.parent = this.transform;
        }
    }

    void Update()
    {
        if (Zombies[0].GetComponent<ZombieController>().state == State.WANDER)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Broadcast();
            }
        }
    }
}