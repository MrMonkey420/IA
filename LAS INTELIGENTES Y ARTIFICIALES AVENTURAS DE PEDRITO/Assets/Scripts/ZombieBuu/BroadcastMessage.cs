using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BroadcastMessage : MonoBehaviour
{
    // Method to be called when a zombie detects a player
    public void OnPlayerDetected()
    {
        // Perform actions when a player is detected, e.g., chase the player
        // ...
        //Debug.Log("Player detected!");
    }

    // Method to be called when a zombie loses sight of the player
    public void OnPlayerLost()
    {
        // Perform actions when the player is lost, e.g., stop chasing
        // ...
        //Debug.Log("Player lost!");
    }

    void Start()
    {
        /// Calls the function ApplyDamage with a value of 5
        BroadcastMessage("ApplyDamage", 5.0);
    }

    // Every script attached to the game object and all its children
    // that has a ApplyDamage function will be called.
    void ApplyDamage(float damage)
    {
        print(damage);
    }
}