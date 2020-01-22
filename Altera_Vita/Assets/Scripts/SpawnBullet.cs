using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private Transform spawner;
    public Transform bullet;

    public void Shoot(Vector3 target)
    {
        Transform trans = Instantiate(bullet, spawner.position, spawner.rotation);
        GameObject go = trans.gameObject;
        BulletMover bulletMover = go.GetComponent<BulletMover>();
        bulletMover.target = target;
    }
}
