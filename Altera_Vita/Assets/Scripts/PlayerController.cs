using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Vector3 startingPosition;

    public Transform target;
    public Vector3 offset;

    private Animator animator;
    private Transform chest;

    bool restarting = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        health = maxHealth;
    }

    void Update()
    {
        if (IsDead()) Reset();

        if (!restarting)
        {
            // Player movement
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, 0.05f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, -0.05f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-0.05f, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.05f, 0, 0);
            }
        }
    }

    private void LateUpdate()
    {
        chest.LookAt(target.position);
        chest.rotation = chest.rotation * Quaternion.Euler(offset);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void Reset()
    {
        restarting = true;
        transform.position = startingPosition;
        // Clone code
    }
}