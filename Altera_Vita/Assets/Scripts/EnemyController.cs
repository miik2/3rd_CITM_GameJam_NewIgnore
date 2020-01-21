using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Vector3 startingPosition;
    PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        health = maxHealth;
    }

    void Update()
    {

    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void Reset()
    {
        transform.position = startingPosition;
        // Clone code
    }
}