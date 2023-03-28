using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public float speed = 30f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -speed);
    } 

    void Update()
    {
        //Destroy object once past camera
        if (transform.position.z < -100)
        {
            Destroy(this.gameObject);
        }
    }
}
