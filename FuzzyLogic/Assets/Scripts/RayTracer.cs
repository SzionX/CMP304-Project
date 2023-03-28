using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour
{
    //Serialize fields to pass in angle, linerenderer, and ray starting position
    [SerializeField] private float angle;
    [SerializeField] private bool rayOn;
    [SerializeField] GameObject rayPoint;
    [SerializeField] LineRenderer lineRenderer;

    //Public raycasthit variable that can be accessed by other scripts
    public RaycastHit hit;

    void Start()
    {
        rayOn = true;
    }

    void Update()
    {
        //Toggle Raycast lines on or off
        if (rayOn)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        lineRenderer.SetPosition(0, rayPoint.transform.position);
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        Vector3 direction = Quaternion.AngleAxis(angle, rayPoint.transform.forward) * rayPoint.transform.up;

        // Cast a ray forward from the position of this GameObject
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
