using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningShark : Shark
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float speed = 10f;
    private string obstacleTag = "Obstacle";
    private Vector3 obstacleForce;

    private void Update()
    {
        if (boxCollider.bounds.Contains(transform.position))
        {
            transform.Translate(0, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);
        }
        else if (boxCollider.bounds.min.y > transform.position.y)
        {
            transform.Translate(0, speed / 2 * Time.deltaTime, 0);
        }
        else if (boxCollider.bounds.max.y < transform.position.y)
        {
            transform.Translate(0, -speed / 2 * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            rb.AddForce(obstacleForce, ForceMode.Impulse);
        }
    }
}
