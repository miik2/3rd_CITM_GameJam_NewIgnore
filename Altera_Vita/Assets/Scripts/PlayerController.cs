using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Vector3 hipsOffset;
    [SerializeField] private Vector3 chestOffset;
    [SerializeField] private GameObject gun;
    public bool mustRespawn = false;
    public float lastDeathTime = 0.0f;
    public float respawnWait = 3.0f;
    public float resetLevelWait = 6.0f;

    public int health;
    public float speed = 0.025f;
    public Vector3 startingPosition = Vector3.zero;
    public Quaternion startingRotation = Quaternion.identity;
    public Transform target;
    public int max_clip_ammo = 7;
    public int damage = 10;
    private int clip_ammo;

    private Animator animator;
    private Transform chest;
    private Transform hips;
    private float Input_X;
    private float Input_Z;
    private AudioLowPassFilter low_pass_filter;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
        hips = animator.GetBoneTransform(HumanBodyBones.Hips);
        health = maxHealth;
        clip_ammo = max_clip_ammo;
        low_pass_filter = GameObject.Find("Audio_Test").GetComponent<AudioLowPassFilter>();
        low_pass_filter.cutoffFrequency = 22000;
    }

    void Update()
    {
        if (IsDead() || Input.GetKey(KeyCode.F))
        {
            animator.SetBool("IsDead", true);
            lastDeathTime = Time.time;
            mustRespawn = true;
            low_pass_filter.cutoffFrequency = 300;
        }

        // Respawn in case it died
        if (mustRespawn && Time.time >= lastDeathTime + respawnWait)
        {
            ResetCharacter();
            mustRespawn = false;
        }

        if (Time.time >= lastDeathTime + resetLevelWait)
        {       
            // Player movement
            Input_X = Input.GetAxis("Vertical");
            animator.SetFloat("Input_X", Input_X);
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
                Input_X = Input.GetAxis("Horizontal");
                animator.SetFloat("Input_X", Input_X);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed, 0, 0);
                Input_X = Input.GetAxis("Horizontal");
                animator.SetFloat("Input_X", Input_X);
            }

            // Player shoot
            if (Input.GetMouseButtonDown(0) && clip_ammo > 0)
            {
                clip_ammo--;
                gun.GetComponent<SpawnBullet>().Shoot();
            }
            else
            {
                //Maybe play empty gun cocking here since we have no bullets left
            }

            // reload!
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                clip_ammo = max_clip_ammo;
            }
        }
    }

    private void LateUpdate()
    {        
        chest.LookAt(target);
        chest.rotation *= Quaternion.Euler(chestOffset);

       // hips.LookAt(CalculateForwardTransform());        
       // hips.rotation *= Quaternion.Euler(hipsOffset);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void ResetCharacter()
    {
        // Stats reset
        health = maxHealth;
        clip_ammo = max_clip_ammo;

        // Position reset
        transform.position = startingPosition;
        transform.rotation = startingRotation;

        // Animation reset
        animator.SetBool("IsDead", false);
        animator.SetFloat("Input_X", 0.0f);
        animator.SetFloat("Input_Z", 0.0f);

        // Audio effect reset
        low_pass_filter.cutoffFrequency = 22000;

        // Clone code
    }

}