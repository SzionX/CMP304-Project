using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour
{
    [SerializeField] private float angle;
    [SerializeField] GameObject rayPoint;
    [SerializeField] LineRenderer lineRenderer;

    void Update()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, rayPoint.transform.position);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        Vector3 direction = Quaternion.AngleAxis(angle, rayPoint.transform.forward) * rayPoint.transform.up;

        // Cast a ray forward from the position of this GameObject
        RaycastHit hit;
        int layerMask = 1 << 8;

        // If the ray hits a collider, draw a debug ray to visualize it
        if (Physics.Raycast(rayPoint.transform.position, direction, out hit, layerMask))
        {
            lineRenderer.SetPosition(1, hit.point);
            lineRenderer.material.SetColor("_Color", Color.green);
          
        } else
        {
            lineRenderer.SetPosition(1, rayPoint.transform.position + direction * 20f);
            lineRenderer.material.SetColor("_Color", Color.red);
        }
    }
}
