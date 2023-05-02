using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStraight : MonoBehaviour
{
    [SerializeField] private float moveSpeed = -1.0f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed;
    }
}
