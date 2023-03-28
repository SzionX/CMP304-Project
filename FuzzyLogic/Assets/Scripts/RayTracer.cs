using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracer : MonoBehaviour
{

    public GameObject pointer;

    void Update()
    {
        // Cast a ray forward from the position of this GameObject
        RaycastHit hit;
        //int layerMask = 1 << 8;

        // If the ray hits a collider, draw a debug ray to visualize it
        if (Physics.Raycast(pointer.transform.position, pointer.transform.TransformDirection(Vector3.forward), out hit, 20f))
        {
     
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.DrawLine(pointer.transform.position, pointer.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        } else
        {
            Debug.Log("Hit Nothing");
            Debug.DrawLine(pointer.transform.position, pointer.transform.TransformDirection(Vector3.forward) * 20f, Color.red);
        }
    }
}
