using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate = 0.3f;
    private float fireTime;


    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Time.time > fireTime)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            fireTime = Time.time + fireRate; // Fire rate cooldown
        }
    }
}
