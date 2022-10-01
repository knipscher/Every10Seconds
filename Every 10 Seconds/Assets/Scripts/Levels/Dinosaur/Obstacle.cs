using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private string obstacleTag = "Obstacle";
    [SerializeField] private float speed = -10f;

    private void Awake()
    {
        tag = obstacleTag;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * GameManager.instance.level, 0, 0);
    }
}
