﻿using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;
    private Rigidbody rig;

    Vector3 direction;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - Camera.main.transform.position).magnitude;

        Vector3 destination = mouseRay.origin + mouseRay.direction * midPoint;
        destination.y = transform.position.y;

        direction = (destination - transform.position).normalized;

    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}