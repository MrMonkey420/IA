using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    [SerializeField] private float timer = 1f;

    public FlockManager myManager;

    [SerializeField] private float speed;
    [SerializeField] private Vector3 CohesionPoint;
    [SerializeField] private Vector3 SeparationPoint;
    [SerializeField] private Vector3 AlignmentPoint;
    [SerializeField] private Vector3 Direction = Vector3.up;

    //-----------------------------------------------------------
    private void Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.Fishes)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;

        CohesionPoint = cohesion;
    }
    private void Separation()
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject go in myManager.Fishes)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) / (distance * distance);
            }
        }

        SeparationPoint = separation;
    }
    private void Alignment()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.Fishes)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    align += go.GetComponent<BoidBehavior>().Direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }

        AlignmentPoint = align;
    }

    //------------------------------------------------------------
    void Start()
    {

    }
    void Update()
    {
        timer += Time.deltaTime;

        Cohesion();
        Separation();
        Alignment();

        Direction = (CohesionPoint + AlignmentPoint + SeparationPoint).normalized * speed;

        if (timer >= 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Direction), myManager.rotationSpeed * Time.deltaTime);
            transform.Translate((Time.deltaTime * speed * Direction));
        }
    }
}