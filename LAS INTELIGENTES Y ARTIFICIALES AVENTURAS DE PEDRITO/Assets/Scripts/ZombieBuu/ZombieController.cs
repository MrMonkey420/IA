using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    WANDER,
    CHASING
}

public class ZombieController : MonoBehaviour
{
    public State state;

    void Start()
    {
        state = State.WANDER;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) state=State.CHASING;
        //detectar player
        //broadcastmessage
    }
}
