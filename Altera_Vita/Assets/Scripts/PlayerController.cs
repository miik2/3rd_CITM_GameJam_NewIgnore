using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Vector3 offset;
    private float lastTime = 0.0f;
    public float waitTime = 2.0f;

    public int health;
    public float speed = 0.025f;
    public Vector3 startingPosition;
    public Transform target;

    private Animator animator;
    private Transform chest;
    private float Input_X;
    private float Input_Z;

    bool restarting = false;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        health = maxHealth;
    }

    void Update()
    {
        if (IsDead() || Input.GetKeyDown(KeyCode.R)) Reset();

        if (Time.time >= lastTime + waitTime)
        {
            // Player movement
            Input_Z = Input.GetAxis("Vertical");
            animator.SetFloat("Input_Z", Input_Z);

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0, 0, speed);
                Input_Z = Input.GetAxis("Vertical");
                animator.SetFloat("Input_Z", Input_Z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(0, 0, -speed);
                Input_Z = Input.GetAxis("Vertical");
                animator.SetFloat("Input_Z", Input_Z);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0);
            }
        }
    }

    private void LateUpdate()
    {        
        chest.LookAt(target);
        chest.rotation = chest.rotation * Quaternion.Euler(offset);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void Reset()
    {
        restarting = true;
        lastTime = Time.time;
        transform.position = startingPosition;
        // Clone code
    }
}