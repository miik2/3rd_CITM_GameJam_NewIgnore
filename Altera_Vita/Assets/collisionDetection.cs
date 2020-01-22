using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string tmp = other.transform.name;
    }
}
