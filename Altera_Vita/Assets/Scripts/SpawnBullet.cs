using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    public Transform bullet;

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        Instantiate(bullet, spawner.position, spawner.rotation);
    }
}
