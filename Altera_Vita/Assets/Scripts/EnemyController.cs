using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum state
    {
        IDLE,
        ATTACKING,
        SEEKING,
        DEAD
    } public state status = state.IDLE;
    public int maxHealth = 100;
    public int health;
    public Vector3 startingPosition = Vector3.zero;
    public Quaternion startingRotation = Quaternion.identity;

    public GameObject target = null;
    public Vector3 last_location = Vector3.zero;

    public Shot_Collector shotCollector;

    public AIPercieve percieve;
    public AIPerceptionManager perception_manager;

    private PlayerController player;
    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        animator = gameObject.GetComponent<Animator>();
        health = maxHealth;
        GameObject gameController = GameObject.Find("GameController");
        gameController.GetComponent<ManageScene>().enemies.Add(this);
        shotCollector = gameController.GetComponent<Shot_Collector>();
    }

    public bool ScanForPlayers()
    {
        foreach (GameObject go in percieve.detected)
        {
            if (LayerMask.LayerToName(go.layer) == "Player" && !go.GetComponent<PlayerController>().IsDead())
            {
                target = go;
                return true;
            }
        }
            
        return false;
    }

    public bool ScanShotsFired()
    {
        foreach (Shot_Collector.Shot shot_it in shotCollector.shotLocations)
        {
            Vector3 shot_pos = shot_it.author.transform.position;

            if (LayerMask.LayerToName(shot_it.author.layer) == "Player" && Vector3.Distance(shot_pos, gameObject.transform.position) < percieve.hear_distance)
            {
                last_location = shot_pos;
                return true;
            }
        }

        return false;
    }

    public bool IsTargetDead()
    {
        return target.GetComponent<PlayerController>().IsDead();
    }

    public void FireAtTarget()
    {
        //Shot

        if (IsTargetDead())
            if (!ScanForPlayers())
                target = null;
    }

    //if (sight)
    //    {
    //        foreach (GameObject go in detected)
    //            if (LayerMask.LayerToName(go.layer) == "Enemies")
    //                if (go.GetComponent<EnemyController>().status == EnemyController.state.DEAD)
    //                {
    //                    manager.shotHeard = true;
    //                    manager.last_location = GameObject.Find("Player").;
    //                }
    //    }
    //    else if (hearing)
    //    {
    //        foreach (Shot_Collector.Shot shot_it in shotCollector.shotLocations)
    //            if (Vector3.Distance(gameObject.transform.position, shot_it.author.transform.position) < hear_distance)
    //            {
    //                manager.shotHeard = true;
    //                manager.location = shot_it.author.transform.position;
    //            }
    //    }

    //    //////

    //    if (LayerMask.LayerToName(ev.target.layer) == "Player") //The only NEW detection event we care about is player
    //    {
    //        controller.target = ev.target;
    //    }

    //    ////////
    //    ///

    //    if (controller.target == ev.target)
    //    {
    //        bool found = false;

    //        foreach (GameObject go in perception.detected)
    //        {
    //            if (LayerMask.LayerToName(go.layer) == "Player")    //Attempt to get another player
    //            {
    //                controller.target = go;
    //                found = true;
    //                break;
    //            }
    //        }

    //        if (!found)
    //            controller.target = null;   //
    //    }

    void Update()
    {
        if (IsDead())
        {
            animator.SetBool("IsDead", true);
        }
        else
        {
            IsTargetDead();
        }


        //if (LayerMask.LayerToName(ev.target.layer) == "Player") //The only NEW detection event we care about is player
        //{
        //    controller.target = ev.target;
        //}

        //////////
        /////

        //if (controller.target == ev.target)
        //{
        //    bool found = false;

        //    foreach (GameObject go in perception.detected)
        //    {
        //        if (LayerMask.LayerToName(go.layer) == "Player")    //Attempt to get another player
        //        {
        //            controller.target = go;
        //            found = true;
        //            break;
        //        }
        //    }

        //    if (!found)
        //        controller.target = null;   //
        //}
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