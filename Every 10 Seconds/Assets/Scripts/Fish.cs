using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(speed, 0, 0);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Shark shark))
        {
            GameManager.instance.Score(pointValue);
            Destroy(gameObject);
        }
    }
}
