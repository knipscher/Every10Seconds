using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Spawn spawn))
        {
            Destroy(spawn.gameObject);
        }
    }
}
