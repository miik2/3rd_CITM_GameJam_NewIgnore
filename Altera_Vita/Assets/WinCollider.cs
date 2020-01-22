using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int a = 0;
        }
    }
}
