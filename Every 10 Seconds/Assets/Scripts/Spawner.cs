using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns = 1;
    [SerializeField] private bool randomizeSpawnTime = true;
    [SerializeField] private GameObject prefabToSpawn;
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            var timeToWait = timeBetweenSpawns;
            if (randomizeSpawnTime)
            {
                timeToWait = Random.Range(timeBetweenSpawns, timeBetweenSpawns * 2);
            }

            yield return new WaitForSeconds(timeToWait);

            var spawnX = Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x);
            var spawnY = Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y);
            var spawnZ = 0;
            var spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

            var spawn = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            spawn.transform.SetParent(transform);
        }
    }
}
