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
    public ZombieManager manager;
    public State state;

    private void FollowPlayer() { state = State.CHASING; }
    void Start()
    {
        state = State.WANDER;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) FollowPlayer();
        //detectar player
    }
}
