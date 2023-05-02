using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //Spawner variables
    [SerializeField] GameObject asteroid;
    [SerializeField] Vector3 spawnValues;
    [SerializeField] float spawnWaitTime;
    [SerializeField] float startWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(startWaitTime);
        
        while (true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(asteroid, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWaitTime);
        }
    }
}
