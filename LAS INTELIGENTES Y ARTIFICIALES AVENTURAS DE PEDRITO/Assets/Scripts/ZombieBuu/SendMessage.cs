using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    void Start()
    {
        // Calls the function ApplyDamage with a value of 5
        // Every script attached to the GameObject
        // that has an ApplyDamage function will be called.
        gameObject.SendMessage("ApplyDamage", 5.0);
    }
    /*
    void Update()
    {
        if (//player lost condition)
        {
            // Notify other zombies that the player is lost
            BroadcastMessage("OnPlayerLost", SendMessageOptions.DontRequireReceiver);
        }
    } */

} 
public class Example2 : MonoBehaviour
{
    public void ApplyDamage(float damage)
    {
        print(damage);
    }
}