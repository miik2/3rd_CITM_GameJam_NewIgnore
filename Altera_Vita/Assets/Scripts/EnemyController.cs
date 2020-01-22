using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Vector3 startingPosition = Vector3.zero;
    public Quaternion startingRotation = Quaternion.identity;

    private PlayerController player;
    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        health = maxHealth;
        GameObject.Find("GameController").GetComponent<ManageScene>().enemies.Add(this);
    }

    void Update()
    {
        if (IsDead())
        {
            animator.SetBool("IsDead", true);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        Destroy(other.gameObject);
        health -= player.damage;
    }

    public void ResetEnemy()
    {
        health = maxHealth;
        transform.position = startingPosition;
        transform.rotation = startingRotation;
        animator.SetBool("IsDead", false);
    }
}