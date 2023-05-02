using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHazardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float radius = 5;
    [SerializeField] private float spawnRate = 3.0f;
    private float spawnTimer;


    public float spawnDistance = 50f;
    private float timer = 0f;

    public float minX, maxX, minY, maxY;


    void Update()
    {
        //SpawnAtRandomLocation();

        timer += Time.deltaTime;
        if (timer >= spawnTimer)
        {
            //spawn asteroids
            SpawnRandomly();
            timer = 0f;
        }
    }

    private void SpawnAtRandomLocation()
    {
        //Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * radius;
        //Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        if (Time.time > spawnTimer && GameManager.instance.gameOver == false)
        {
            Vector3 randomPos = UnityEngine.Random.insideUnitSphere * radius;
            randomPos += transform.position;
            randomPos.y = 0f;

            Vector3 direction = randomPos - transform.position;
            direction.Normalize();

            float dotProduct = Vector3.Dot(transform.forward, direction);
            float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

            randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
            randomPos.z = Mathf.Sin(dotProductAngle * (UnityEngine.Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.z;

            GameObject go = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            go.transform.position = randomPos;
            spawnTimer = Time.time + spawnRate;
        }
    }

    private void SpawnRandomly()
    {
        float newX = UnityEngine.Random.Range(minX, maxX);
        float newY = UnityEngine.Random.Range(minX, maxY);

        Vector3 spawPos = new Vector3(newX, newY, spawnDistance);

        Instantiate(enemyPrefab, spawPos, Quaternion.identity);
    }
}
