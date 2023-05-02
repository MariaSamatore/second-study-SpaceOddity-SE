using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 4.0f; 
    public GameObject hazardPrefab;
    private float spawnTimer;

    void Update()
    {
        SpawnHazard();
    }

    private void SpawnHazard()
    {
        if (Time.time > spawnTimer && GameManager.instance.gameOver == false) 
        {
            Instantiate(hazardPrefab, transform.position, transform.rotation);
            spawnTimer = Time.time + spawnRate;
        }
    }
}
