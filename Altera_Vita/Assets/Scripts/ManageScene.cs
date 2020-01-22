using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScene : MonoBehaviour
{
    public List<EnemyController> enemies;
    private PlayerController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.mustRespawn /*&& Time.time >= player.lastDeathTime + player.respawnWait*/)
        {
            foreach (EnemyController enemy in enemies)
            {
                enemy.ResetEnemy();
            }
        }
    }
}
