using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Vector3 spawnAreaSize;
    public float spawnInterval = 30.0f; // Spawn every 30 seconds
    public float objectSpeed = 5.0f; // Speed of the spawned objects
    public int maxObjectCount = 2; // Maximum number of spawned objects

    private int currentObjectCount = 0;

    private void Start()
    {
        // Start spawning objects at intervals.
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (currentObjectCount < maxObjectCount)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
                currentObjectCount++;

                // Add movement to the spawned object.
                Vector3 randomDirection = Random.insideUnitSphere.normalized;
                Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();

                if (rb == null)
                {
                    // Add a Rigidbody component if not already present.
                    rb = spawnedObject.AddComponent<Rigidbody>();
                }

                rb.velocity = randomDirection * objectSpeed;

                StartCoroutine(DestroyObjectAfterDelay(spawnedObject, 30.0f)); // Destroy the object after 30 seconds.
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        currentObjectCount--;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnAreaCenter = transform.position;
        Vector3 minSpawnPoint = spawnAreaCenter - spawnAreaSize / 2;
        Vector3 maxSpawnPoint = spawnAreaCenter + spawnAreaSize / 2;

        float randomX = Random.Range(minSpawnPoint.x, maxSpawnPoint.x);
        float randomY = Random.Range(minSpawnPoint.y, maxSpawnPoint.y);
        float randomZ = Random.Range(minSpawnPoint.z, maxSpawnPoint.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
