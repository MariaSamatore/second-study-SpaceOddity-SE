using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    // Any game object that leaves the boundary will get destroyed for efficiency
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
