using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Null reference check
        if (GameManager.instance.player)
        {
            //Enemy looks at the player while the player is alive and moves toward it
            transform.LookAt(GameManager.instance.player.transform.position);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
}
