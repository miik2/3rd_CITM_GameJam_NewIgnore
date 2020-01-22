using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{

    public Transition anim;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int a = 0;
            anim.LoadNextLevel();
        }
    }
}
