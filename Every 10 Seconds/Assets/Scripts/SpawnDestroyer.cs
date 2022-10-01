using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyer : MonoBehaviour
{
    [SerializeField]
    private string[] spawnTags;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var spawnTag in spawnTags)
        {
            if (collision.gameObject.CompareTag(spawnTag))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
