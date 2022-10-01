using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isInWater = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (isInWater)
        {
            rb.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var force = new Vector3(horizontal, vertical, 0);
        rb.AddForce(force * speed);
    }
}
