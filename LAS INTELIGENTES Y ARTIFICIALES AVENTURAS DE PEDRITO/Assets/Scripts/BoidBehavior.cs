using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class BoidBehavior : MonoBehaviour
{
    public FlockManager myManager;
    private float timer = 0f;
    public float speed;
    bool OutOfBounds = false;

    [SerializeField] private Vector3 avoidPoint = Vector3.zero;
    void ChangeDirection()
    {
        Vector3 direction;
        float distance;
        float nums = 0;
        float auxSpeed = 0;

        foreach (GameObject go in myManager.Fishes)
        {
            if(go != this.gameObject)
            {
                distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    nums++;
                    if(distance < myManager.minDistance) avoidPoint += transform.position - go.transform.position;
                }
                auxSpeed += go.GetComponent<BoidBehavior>().speed;
            }
        }

        if(nums > 0)
        {
            speed = auxSpeed / nums;

            if(speed >= myManager.maxSpeed) speed = myManager.maxSpeed;
            //if(speed <= myManager.minSpeed) speed = myManager.minSpeed;

            direction = myManager.centre - avoidPoint - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
        }
    }

    //------------------------------------------------------------
    void Start()
    {
        speed = UnityEngine.Random.Range(myManager.minSpeed, myManager.maxSpeed);
    }
    void Update()
    {
        float distanceToCenter = Vector3.Distance(transform.position, myManager.transform.position);

        if (distanceToCenter >= myManager.neighbourDistance) OutOfBounds = true;
        else OutOfBounds = false;

        if(OutOfBounds)
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotSpeed * Time.deltaTime);
        }
        else
        {
            if(UnityEngine.Random.Range(0, 10) < 1) speed = UnityEngine.Random.Range(myManager.minSpeed, myManager.maxSpeed);
            else ChangeDirection();
        }

        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}