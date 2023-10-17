using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour
{
    public ZombieManager manager;
    public Camera frustum;
    public LayerMask mask;
    public bool PlayerDetected = false;

    private void Start()
    {
        manager = GetComponent<ZombieManager>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray();
                ray.origin = transform.position;
                ray.direction = (col.transform.position - transform.position).normalized;
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        manager.Broadcast();
                    }
                }
            }
        }
    }
}